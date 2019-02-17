using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.Extensions;
using Gym.Infrastructure.Repositories;
using Gym.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Gym.Tests.Services
{
    public class TrainingPlanServiceTests
    {
/*        [Fact]
        public void create_new_training_plan()
        {
            trainingService.CreateNew("NewPlan", FakeDataBase.GetInstance().Exercises);

            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Once);
        }

        [Fact]
        public void create_new_training_plan_with_empty_fields()
        {
            var testCases = new List<Exception>()
            {
                Assert.Throws<Exception>(() =>  trainingService.CreateNew("", FakeDataBase.GetInstance().Exercises)),
                Assert.Throws<Exception>(() =>  trainingService.CreateNew("NewPlan", new List<Exercise>())),
                Assert.Throws<Exception>(() =>  trainingService.CreateNew("NewPlan", null))
            };

            Assert.Equal("Provided name is not correct.", testCases[0].Message);
            Assert.Equal("Provided exercises list cannot be empty", testCases[1].Message);
            Assert.Equal("Provided exercises list cannot be empty", testCases[2].Message);
            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public void get_if_record_not_exist()
        {
            var testCases = new List<Exception>()
            {
                Assert.Throws<Exception>(() => trainingService.Get("")),
                Assert.Throws<Exception>(() => trainingService.Get(Guid.NewGuid())),
                Assert.Throws<Exception>(() => trainingService.GetAll())
            };

            foreach (var testCase in testCases)
                Assert.Equal($"{ErrorsCodes.ItemNotFound}", testCase.Message);
        }

        [Fact]
        public void get_if_record_exist()
        {
            //ToDo
        }

        #region ARRANGE

        private Mock<ITrainingPlanRepository> trainingPlanRepositoryMock;
        private Mock<IMapper> mapperMock;
        private TrainingPlanService trainingService;

        public TrainingPlanServiceTests()
        {
            trainingPlanRepositoryMock = new Mock<ITrainingPlanRepository>();
            mapperMock = new Mock<IMapper>();
            trainingService = new TrainingPlanService(trainingPlanRepositoryMock.Object, mapperMock.Object);
        }

        #endregion*/
    }
}
