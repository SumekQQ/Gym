using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.Repositories;
using Gym.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Gym.Tests.Services
{
    public class ResultServiceTests
    {
        [Fact]
        public void create_new_results()
        {
            resultService.CreateNew(trainingDay, exercise, 5, 3, 7);

            resultRepositorMock.Verify(x => x.Add(It.IsAny<Result>()), Times.Once);
        }

        [Fact]
        public void create_new_results_using_empty_fields()
        {
            var testCases = new List<Exception>()
            {
                Assert.Throws<Exception>(() => resultService.CreateNew(null, exercise, 5, 3, 7)),
                Assert.Throws<Exception>(() => resultService.CreateNew(trainingDay, null, 5, 3, 7)),
                Assert.Throws<Exception>(() => resultService.CreateNew(trainingDay, exercise, 0, 3, 7)),
                Assert.Throws<Exception>(() => resultService.CreateNew(trainingDay, exercise, 5, 0, 7)),
                Assert.Throws<Exception>(() => resultService.CreateNew(trainingDay, exercise, 5, 3, 0))
            };

            Assert.Equal("Provided traning day not exist.", testCases[0].Message);
            Assert.Equal("Provided exercise not exist.", testCases[1].Message);
            Assert.Equal("Provided amount of series is not correct.", testCases[2].Message);
            Assert.Equal("Provided weights is not correct.", testCases[3].Message);
            Assert.Equal("Provided amount of reps is not correct.", testCases[4].Message);

            resultRepositorMock.Verify(x => x.Add(It.IsAny<Result>()), Times.Never);
        }

        #region ARRANGE

        private Mock<IResultRepository> resultRepositorMock;
        private Mock<ITrainingDayRepository> trainingDayRepositoryMock;
        private Mock<IExerciseRepository> exerciseRepositoryMock;
        private Mock<IMapper> mapperMock;
        private ResultService resultService;

        private TrainingDay trainingDay = FakeDataBase.GetInstance().TrainingDay.First();
        private Exercise exercise = FakeDataBase.GetInstance().Exercises.First();

        public ResultServiceTests()
        {
            resultRepositorMock = new Mock<IResultRepository>();
            trainingDayRepositoryMock = new Mock<ITrainingDayRepository>();
            exerciseRepositoryMock = new Mock<IExerciseRepository>();
            mapperMock = new Mock<IMapper>();
            resultService = new ResultService(resultRepositorMock.Object, trainingDayRepositoryMock.Object, exerciseRepositoryMock.Object, mapperMock.Object);
        }

        #endregion
    }
}
