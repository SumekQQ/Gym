using Gym.Core.Exceptions;
using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Repositories;
using Gym.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gym.Tests.Services
{
    public class ExerciseServiceTests : ServiceTestsTemplate
    {
        [Fact]
        public async Task get_single_exercise_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(Guid.NewGuid())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => exerciseService.Get(Guid.NewGuid()));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task get_colection_exercises_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => exerciseService.GetAll());

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            exerciseRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task get_single_exercise_if_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);

            var exerciseDTO = await exerciseService.Get(Guid.NewGuid());

            Assert.Equal(exerciseDTO, mapperMock.Object.Map<Exercise, ExerciseDTO>(ExampleExercise));
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task get_all_collection_exercise_if_exist()
        {
            exerciseRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(ExampleCollectionExercise);

            var exercisesDTO = await exerciseService.GetAll();

            Assert.Equal(exercisesDTO, mapperMock.Object.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(ExampleCollectionExercise));
            exerciseRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task add_new_exercise()
        {
            await exerciseService.CreateNew("NewExercise", Category.Abs);

            exerciseRepositoryMock.Verify(x => x.Add(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public async Task add_new_exercise_with_empty_name()
        {
            var ex = await Assert.ThrowsAsync<DomainException>(() => exerciseService.CreateNew("", Category.Abs));

            Assert.Equal(ErrorsCodes.IncorrectName, ex.Code);
            exerciseRepositoryMock.Verify(x => x.Add(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public async Task add_new_exercise_when_provided_name_exist()
        {
            var existItem = FakeDataBase.GetInstance().Exercises.First();

            exerciseRepositoryMock.Setup(x => x.IsExist(existItem.Name)).ReturnsAsync(true);
            var ex = await Assert.ThrowsAsync<ServiceException>(() => exerciseService.CreateNew(existItem.Name, existItem.Category));

            Assert.Equal(ErrorsCodes.ItemExist, ex.Code);
            exerciseRepositoryMock.Verify(x => x.Add(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public async Task update_exercise_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => exerciseService.Update(Guid.NewGuid(), "", (Category)0));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public async Task update_exercise_with_empty_name()
        {
            exerciseRepositoryMock.Setup(x => x.Get(ExampleExercise.Id)).ReturnsAsync(ExampleExercise);

            var ex = await Assert.ThrowsAsync<DomainException>(() => exerciseService.Update(ExampleExercise.Id, "", ExampleExercise.Category));

            Assert.Equal(ErrorsCodes.IncorrectName, ex.Code);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public async Task update_cardio_exercise_using_other_category()
        {
            var cardioExercise = ExampleCollectionExercise.First(x => x.Category == Category.Cardio);
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(cardioExercise);

            var ex = await Assert.ThrowsAsync<DomainException>(() => exerciseService.Update(cardioExercise.Id, cardioExercise.Name, (Category)1));

            Assert.Equal(ErrorsCodes.IncorrectCategory, ex.Code);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public async Task update_weight_exercise_using_cardio_category()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);

            var ex = await Assert.ThrowsAsync<DomainException>(() => exerciseService.Update(ExampleExercise.Id, ExampleExercise.Name, Category.Cardio));

            Assert.Equal(ErrorsCodes.IncorrectCategory, ex.Code);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public async Task update_exercise_correctly()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);

            await exerciseService.Update(ExampleExercise.Id, "UpdateExercise", Category.Shoulders);

            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Update(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public async Task delete_exercise_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(ExampleExercise.Id)).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => exerciseService.Delete(ExampleExercise.Id));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public async Task delete_exercise_correctly()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);

            await exerciseService.Delete(ExampleExercise.Id);

            exerciseRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            exerciseRepositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()), Times.Once);
        }

        #region ARRANGE

        private ExerciseService exerciseService;
        private Exercise ExampleExercise;
        private IEnumerable<Exercise> ExampleCollectionExercise;

        public ExerciseServiceTests() : base()
        {
            exerciseService = new ExerciseService(exerciseRepositoryMock.Object, mapperMock.Object);
            ExampleExercise = FakeDataBase.GetInstance().Exercises.First();
            ExampleCollectionExercise = FakeDataBase.GetInstance().Exercises;
        }

        #endregion
    }
}
