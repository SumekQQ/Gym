using Gym.Core.Models;
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
    public class TrainingDayServiceTests : ServiceTestsTemplate
    {
        [Fact]
        public void create_new_training_day_correctly()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingPlan>())).Returns(false);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<DateTime>())).Returns(false);

            trainingDayService.CreateNew(ExampleTrainingPlan.Id, "description", DateTime.Now.ToString());

            trainingDayRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Once);
        }

        [Fact]
        public void create_new_training_using_null_plan()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingPlan>())).Returns(false);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<DateTime>())).Returns(false);

            Exception ex = Assert.Throws<Exception>(() => trainingDayService.CreateNew(ExampleTrainingPlan.Id, "description", DateTime.Now.ToString()));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingDayRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public void create_new_training_day_if_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingPlan>())).Returns(true);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<DateTime>())).Returns(true);

            Exception ex = Assert.Throws<Exception>(() => trainingDayService.CreateNew(ExampleTrainingPlan.Id, "description", DateTime.Now.ToString()));

            Assert.Equal($"{ErrorsCodes.ItemExist}", ex.Message);
            trainingDayRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public void get_single_not_exist_day()
        {
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            Exception ex = Assert.Throws<Exception>(() => trainingDayService.Get(ExampleTrainingDay.Id));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
        }

        [Fact]
        public void get_collection_using_null_plan()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            Exception ex = Assert.Throws<Exception>(() => trainingDayService.GetCollection(ExampleTrainingPlan.Id));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
        }

        [Fact]
        public void get_single_correctly()
        {
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingDay);

            var trainingDay = trainingDayService.Get(ExampleTrainingDay.Id);

            Assert.Equal(trainingDay, mapperMock.Object.Map<TrainingDay, TrainingDayDTO>(ExampleTrainingDay));
        }

        [Fact]
        public void get_collection_correctly()
        {
            var trainingDaysDb = FakeDataBase.GetInstance().TrainingDay.Where(x => x.TrainingPlan == ExampleTrainingPlan);
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<TrainingPlan>())).Returns(trainingDaysDb);

            var trainingDays = trainingDayService.GetCollection(ExampleTrainingPlan.Id);

            Assert.Equal(trainingDays, mapperMock.Object.Map<IEnumerable<TrainingDay>, IEnumerable<TrainingDayDTO>>(trainingDaysDb));
        }

        [Fact]
        public void update_using_empty_plan()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingDay);

            Exception ex = Assert.Throws<Exception>(() => trainingDayService.Update(ExampleTrainingDay.Id, ExampleTrainingPlan.Id, "description"));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingDayRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public void update_if_day_not_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            Exception ex = Assert.Throws<Exception>(() => trainingDayService.Update(ExampleTrainingDay.Id, ExampleTrainingPlan.Id, "description"));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingDayRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public void update_correctly()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingDay);

            trainingDayService.Update(ExampleTrainingDay.Id, ExampleTrainingPlan.Id, "description");

            trainingDayRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingDay>()), Times.Once);
        }

        [Fact]
        public void delete_not_exist_item()
        {
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            Exception ex = Assert.Throws<Exception>(() => trainingDayService.Delete(ExampleTrainingDay.Id));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingDayRepositoryMock.Verify(x => x.Delete(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public void delete_correctly()
        {
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingDay);

            trainingDayService.Delete(ExampleTrainingDay.Id);

            trainingDayRepositoryMock.Verify(x => x.Delete(It.IsAny<TrainingDay>()), Times.Once);
        }

        #region ARRANGE

        private TrainingDayService trainingDayService;
        private TrainingPlan ExampleTrainingPlan;
        private TrainingDay ExampleTrainingDay;

        public TrainingDayServiceTests() : base()
        {
            trainingDayService = new TrainingDayService(trainingDayRepositoryMock.Object, trainingPlanRepositoryMock.Object, mapperMock.Object);
            ExampleTrainingPlan = FakeDataBase.GetInstance().TrainingPlans.First();
            ExampleTrainingDay = FakeDataBase.GetInstance().TrainingDay.First();
        }

        #endregion*/
    }
}
