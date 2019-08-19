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
    public class TrainingDayServiceTests : ServiceTestsTemplate
    {
        [Fact]
        public async Task get_single_training_day_if_not_exist()
        {
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingDayService.Get(ExampleTrainingDay.Id));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingDayRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task get_single_training_day_correctly()
        {
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);

            var trainingDay = await trainingDayService.Get(ExampleTrainingDay.Id);

            Assert.Equal(trainingDay, mapperMock.Object.Map<TrainingDay, TrainingDayDTO>(ExampleTrainingDay));
            trainingDayRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task get_collection_trainig_days_if_not_exist()
        {
            var trainingDaysDb = FakeDataBase.GetInstance().TrainingDay.Where(x => x.TrainingPlan == ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingDayService.GetAll());

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingDayRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task get_collection_trainig_days()
        {
            var trainingDaysDb = FakeDataBase.GetInstance().TrainingDay.Where(x => x.TrainingPlan == ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(trainingDaysDb);

            var trainingDays = await trainingDayService.GetAll();

            Assert.Equal(trainingDays, mapperMock.Object.Map<IEnumerable<TrainingDay>, IEnumerable<TrainingDayDTO>>(trainingDaysDb));
            trainingDayRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task create_new_training_day()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingPlan>())).ReturnsAsync(false);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<DateTime>())).ReturnsAsync(false);

            await trainingDayService.CreateNew(ExampleTrainingPlan.Id, "description", DateTime.Now.ToString());

            trainingDayRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Once);
        }

        [Fact]
        public async Task create_new_training_days_using_null_plan()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingPlan>())).ReturnsAsync(false);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<DateTime>())).ReturnsAsync(false);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingDayService.CreateNew(ExampleTrainingPlan.Id, "description", DateTime.Now.ToString()));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingDayRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public async Task create_new_training_day_if_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingPlan>())).ReturnsAsync(true);
            trainingDayRepositoryMock.Setup(x => x.IsExist(It.IsAny<DateTime>())).ReturnsAsync(true);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingDayService.CreateNew(ExampleTrainingPlan.Id, "description", DateTime.Now.ToString()));

            Assert.Equal(ErrorsCodes.ItemExist, ex.Code);
            trainingDayRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public async Task update_training_day_using_empty_plan()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingDayService.Update(ExampleTrainingDay.Id, ExampleTrainingPlan.Id, "description"));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingDayRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public async Task update_training_day_if_day_not_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingDayService.Update(ExampleTrainingDay.Id, ExampleTrainingPlan.Id, "description"));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingDayRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public async Task update_training_day()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);

            await trainingDayService.Update(ExampleTrainingDay.Id, ExampleTrainingPlan.Id, "description");

            trainingDayRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingDay>()), Times.Once);
        }

        [Fact]
        public async Task delete_training_day_not_exist_item()
        {
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingDayService.Delete(ExampleTrainingDay.Id));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingDayRepositoryMock.Verify(x => x.Delete(It.IsAny<TrainingDay>()), Times.Never);
        }

        [Fact]
        public async Task delete_training_day()
        {
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);

            await trainingDayService.Delete(ExampleTrainingDay.Id);

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
