using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using Gym.Infrastructure.Repositories;
using Gym.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Gym.Tests.Services
{
    public class ExerciseServiceTests
    {
        [Fact]
        public void add_new_exercise()
        {
            exerciseService.CreateNew("NewExercise", Category.Abs);

            exerciseRepositoryMock.Verify(x => x.Add(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public void add_new_exercise_with_empty_name()
        {
            Exception ex = Assert.Throws<Exception>(() => exerciseService.CreateNew("", Category.Abs));
            Assert.Equal("Provided exercise name is not correct.", ex.Message);
            exerciseRepositoryMock.Verify(x => x.Add(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void add_new_exercise_when_provided_name_exist()
        {
            var existItem = FakeDataBase.GetInstance().Exercises.First();

            exerciseRepositoryMock.Setup(x => x.IsExist(existItem.Name)).Returns(true);
            Exception ex = Assert.Throws<Exception>(() => exerciseService.CreateNew(existItem.Name, existItem.Category));

            Assert.Equal($"{ErrorsCodes.ItemExist}", ex.Message);
            exerciseRepositoryMock.Verify(x => x.Add(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void get_single_exercise_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get("")).Returns(value: null);
            exerciseRepositoryMock.Setup(x => x.Get(Guid.NewGuid())).Returns(value: null);

            var testCases = new List<Exception>()
            {
                Assert.Throws<Exception>(() => exerciseService.Get("")),
                Assert.Throws<Exception>(() => exerciseService.Get(Guid.NewGuid())),
            };

            foreach (var testCase in testCases)
                Assert.Equal($"Finding data not exist or return null value", testCase.Message);

            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void get_colection_exercises_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get((Category)1)).Returns(value: null);
            exerciseRepositoryMock.Setup(x => x.Get((Category)2)).Returns(new List<Exercise>());

            var testCases = new List<Exception>()
            {
                Assert.Throws<Exception>(() => exerciseService.Get((Category)1)),
                Assert.Throws<Exception>(() => exerciseService.Get((Category)2)),
                Assert.Throws<Exception>(() => exerciseService.GetAll()),
            };

            foreach (var testCase in testCases)
                Assert.Equal($"Finding data not exist or return null value", testCase.Message);

            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Category>()), Times.AtLeast(2));
        }

        [Fact]
        public void get_single_exercise_if_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get("")).Returns(ExampleExercise);

            var exerciseDTO = exerciseService.Get("");

            Assert.Equal(exerciseDTO, mapperMock.Object.Map<Exercise, ExerciseDTO>(ExampleExercise));
            exerciseRepositoryMock.Verify(x => x.Get(""), Times.Once);
        }

        [Fact]
        public void get_all_collection_exercise_if_exist()
        {
            exerciseRepositoryMock.Setup(x => x.GetAll()).Returns(ExampleCollectionExercise);

            var exercisesDTO = exerciseService.GetAll();

            Assert.Equal(exercisesDTO, mapperMock.Object.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(ExampleCollectionExercise));
            exerciseRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void get_category_collection_exercise_if_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(Category.Legs)).Returns(ExampleCollectionExercise.Where(x => x.Category == Category.Legs));

            var exercisesDTO = exerciseService.Get(Category.Legs);

            Assert.Equal(exercisesDTO, mapperMock.Object.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(ExampleCollectionExercise.Where(x => x.Category == Category.Legs)));
            exerciseRepositoryMock.Verify(x => x.Get(Category.Legs), Times.Once);
        }

        [Fact]
        public void update_exercise_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(ExampleExercise.Id)).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => exerciseService.Update(ExampleExercise.Id, "", (Category)0));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void update_exercise_with_empty_name()
        {
            exerciseRepositoryMock.Setup(x => x.Get(ExampleExercise.Id)).Returns(ExampleExercise);

            var ex = Assert.Throws<Exception>(() => exerciseService.Update(ExampleExercise.Id, "", ExampleExercise.Category));

            Assert.Equal("Provided exercise name is not correct.", ex.Message);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void update_cardio_exercise_using_other_category()
        {
            var cardioExercise = ExampleCollectionExercise.First(x => x.Category == Category.Cardio);
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(cardioExercise);

            var ex = Assert.Throws<Exception>(() => exerciseService.Update(cardioExercise.Id, cardioExercise.Name, (Category)1));

            Assert.Equal("Can not update category exercise asigned to cardio category.", ex.Message);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void update_weight_exercise_using_cardio_category()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleExercise);

            var ex = Assert.Throws<Exception>(() => exerciseService.Update(ExampleExercise.Id, ExampleExercise.Name, Category.Cardio));

            Assert.Equal("Can not update to cardio category exercise asigned to weight category.", ex.Message);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void update_exercise_correctly()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleExercise);

            exerciseService.Update(ExampleExercise.Id, "UpdateExercise", Category.Shoulders);

            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public void delete_exercise_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(ExampleExercise.Id)).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => exerciseService.Delete(ExampleExercise.Id));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void delete_exercise_correctly()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleExercise);

            exerciseService.Delete(ExampleExercise.Id);

            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()), Times.Once);
        }

        #region ARRANGE

        private Mock<IExerciseRepository> exerciseRepositoryMock;
        private Mock<IMapper> mapperMock;
        private ExerciseService exerciseService;
        private Exercise ExampleExercise;
        private IEnumerable<Exercise> ExampleCollectionExercise;

        public ExerciseServiceTests()
        {
            exerciseRepositoryMock = new Mock<IExerciseRepository>();
            mapperMock = new Mock<IMapper>();
            exerciseService = new ExerciseService(exerciseRepositoryMock.Object, mapperMock.Object);
            ExampleExercise = FakeDataBase.GetInstance().Exercises.First();
            ExampleCollectionExercise = FakeDataBase.GetInstance().Exercises;
        }

        #endregion
    }
}
