using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
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
            exerciseService.CreateNew("NewExercise", BodyPart.Arms);

            exerciseRepositoryMock.Verify(x => x.Add(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public void add_new_exercise_with_empty_name()
        {
            Exception ex = Assert.Throws<Exception>(() => exerciseService.CreateNew("", BodyPart.Arms));
            Assert.Equal("Provided exercise name is not correct.", ex.Message);
            exerciseRepositoryMock.Verify(x => x.Add(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void add_new_exercise_when_provided_name_exist()
        {
            var existItem = FakeDataBase.GetInstance().Exercises.First();

            exerciseRepositoryMock.Setup(x => x.IsExist(existItem.Name)).Returns(true);
            Exception ex = Assert.Throws<Exception>(() => exerciseService.CreateNew(existItem.Name, existItem.BodyPart));

            Assert.Equal($"{ErrorsCodes.ItemExist}", ex.Message);
        }

        [Fact]
        public void get_exercise_if_not_exist()
        {
            var testCases = new List<Exception>()
            {
                Assert.Throws<Exception>(() => exerciseService.Get("")),
                Assert.Throws<Exception>(() => exerciseService.Get(Guid.NewGuid())),
                Assert.Throws<Exception>(() => exerciseService.Get((BodyPart)1))
            };

            foreach (var testCase in testCases)
                Assert.Equal($"{ErrorsCodes.ItemNotFound}", testCase.Message);
        }

        [Fact]
        public void get_exercise_if_exist()
        {
         /* var exercises = FakeDataBase.GetInstance().Exercises;
            exercises.ForEach(exercise => exerciseService.CreateNew(exercise.Name, exercise.BodyPart));
            exercises.ForEach(exercise => exerciseRepositoryMock.Setup(x => x.Add(exercise)));

            Assert.Equal(exerciseService.Get(exercises.First().Id).Id, exercises.First().Id);
            Assert.Equal(exerciseService.Get(exercises.Last().Name).Name, exercises.Last().Name);
            Assert.Equal(exerciseService.Get((BodyPart)1).Count(), exercises.Where(x => x.BodyPart == (BodyPart)1).Count());*/
        }

        #region ARRANGE

        private Mock<IExerciseRepository> exerciseRepositoryMock;
        private Mock<IMapper> mapperMock;
        private ExerciseService exerciseService;

        public ExerciseServiceTests()
        {
            exerciseRepositoryMock = new Mock<IExerciseRepository>();
            mapperMock = new Mock<IMapper>();
            exerciseService = new ExerciseService(exerciseRepositoryMock.Object, mapperMock.Object);
        }

        #endregion
    }
}
