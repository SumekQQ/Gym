using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.Repositories;
using Gym.Infrastructure.Services;
using Moq;
using System;
using System.Linq;
using Xunit;
namespace Gym.Tests.Services
{
    public class TrainingDayServiceTests
    {
/*        [Fact]
        public void create_new_training_day()
        {
            trainingDayService.CreateNew(FakeDataBase.GetInstance().TrainingPlans.First(), "description");

            trainingDayRepositorMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Once);
        }

        [Fact]
        public void create_new_training_day_with_empty_fields()
        {
            Exception ex = Assert.Throws<Exception>(() => trainingDayService.CreateNew(null, "description"));

            Assert.Equal("Provided training plan cannot be empty", ex.Message);
            trainingDayRepositorMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public void create_new_training_day_if_exist()
        {
           trainingDayRepositorMock.Setup(x => x.IsExist(DateTime.UtcNow)).Returns(true);
            trainingDayRepositorMock.Setup(x => x.IsExist(FakeDataBase.GetInstance().TrainingPlans.First())).Returns(true);
            Exception ex = Assert.Throws<Exception>(() => trainingDayService.CreateNew(FakeDataBase.GetInstance().TrainingPlans.First(), "description"));

            Assert.Equal($"{ErrorsCodes.ItemExist}", ex.Message);
            trainingDayRepositorMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Never);
        }


        #region ARRANGE

        private Mock<ITrainingDayRepository> trainingDayRepositorMock;
        private Mock<IMapper> mapperMock;
        private TrainingDayService trainingDayService;

        public TrainingDayServiceTests()
        {
            trainingDayRepositorMock = new Mock<ITrainingDayRepository>();
            mapperMock = new Mock<IMapper>();
            trainingDayService = new TrainingDayService(trainingDayRepositorMock.Object, mapperMock.Object);
        }

        #endregion*/
    }
}
