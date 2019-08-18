using AutoMapper;
using Gym.Core.Exceptions;
using Gym.Core.Models;
using Gym.Core.Repositories;
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
    public class TrainingPlanServiceTests : ServiceTestsTemplate
    {
        [Fact]
        public async Task get_single_if_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);

            var trainingPlan = await trainingService.Get(ExampleTrainingPlan.Id);

            Assert.Equal(trainingPlan, mapperMock.Object.Map<TrainingPlan, TrainingPlanDTO>(ExampleTrainingPlan));
            trainingPlanRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task get_single_if_not_exist()
        {
            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingService.Get(ExampleTrainingPlan.Id));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingPlanRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task get_all_if_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(FakeDataBase.GetInstance().TrainingPlans);

            var trainingPlans = await trainingService.GetAll();

            Assert.Equal(trainingPlans, mapperMock.Object.Map<IEnumerable<TrainingPlan>, IEnumerable<TrainingPlanDTO>>(FakeDataBase.GetInstance().TrainingPlans));
            trainingPlanRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task get_all_if_not_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(value: null);
            var ex1 = await Assert.ThrowsAsync<ServiceException>(() => trainingService.GetAll());

            trainingPlanRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<TrainingPlan>());
            var ex2 = await Assert.ThrowsAsync<ServiceException>(() => trainingService.GetAll());

            Assert.Equal(ErrorsCodes.ItemNotFound, ex1.Code);
            Assert.Equal(ErrorsCodes.ItemNotFound, ex2.Code);
            trainingPlanRepositoryMock.Verify(x => x.GetAll(), Times.AtLeast(2));
        }

        [Fact]
        public async Task add_new_training_plan()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);
            await trainingService.CreateNew("NewPlan", ExampleGuidsExercise);

            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Once);
        }

        [Fact]
        public async Task add_new_training_plan_with_empty_fields()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);
            var testCases = new List<GymException>()
            {
                await Assert.ThrowsAsync<DomainException>(() =>  trainingService.CreateNew("", ExampleGuidsExercise )),
                await Assert.ThrowsAsync<ServiceException>(() =>  trainingService.CreateNew("NewPlan", new List<Guid>())),
                await Assert.ThrowsAsync<ServiceException>(() =>  trainingService.CreateNew("NewPlan", null))
            };

            Assert.Equal(ErrorsCodes.IncorrectName, testCases[0].Code);
            Assert.Equal(ErrorsCodes.ItemNotFound, testCases[1].Code);
            Assert.Equal(ErrorsCodes.ItemNotFound, testCases[2].Code);

            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public async Task add_new_training_using_exist_name()
        {
            trainingPlanRepositoryMock.Setup(x => x.IsExist(It.IsAny<string>())).ReturnsAsync(true);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingService.CreateNew(ExampleTrainingPlan.Name, ExampleGuidsExercise));

            Assert.Equal(ErrorsCodes.ItemExist, ex.Code);
            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public async Task add_new_when_some_exercise_return_null()
        {
            ExampleGuidsExercise.Add(Guid.NewGuid());

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingService.CreateNew(ExampleTrainingPlan.Name, ExampleGuidsExercise));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public async Task update_if_not_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingService.Update(ExampleTrainingPlan.Id, ExampleTrainingPlan.Name, ExampleGuidsExercise));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingPlanRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public async Task update_using_empty_name()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(FakeDataBase.GetInstance().Exercises.First());
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);

            var ex = await Assert.ThrowsAsync<DomainException>(() => trainingService.Update(ExampleTrainingPlan.Id, "", ExampleGuidsExercise));

            Assert.Equal(ErrorsCodes.IncorrectName, ex.Code);
            trainingPlanRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public async Task update_using_incorrect_exercise_list()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);
            ExampleGuidsExercise.Add(Guid.NewGuid());

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingService.Update(ExampleTrainingPlan.Id, ExampleTrainingPlan.Name, ExampleGuidsExercise));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingPlanRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public async Task update_correctly()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(FakeDataBase.GetInstance().Exercises.First());
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);
            ExampleGuidsExercise.RemoveAt(4);

            await trainingService.Update(ExampleTrainingPlan.Id, "NewName", ExampleGuidsExercise);

            trainingPlanRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingPlan>()), Times.Once);
        }

        [Fact]
        public async Task delete_if_not_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => trainingService.Delete(Guid.NewGuid()));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            trainingPlanRepositoryMock.Verify(x => x.Delete(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public async Task delete_correctly()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingPlan);

            await trainingService.Delete(ExampleTrainingPlan.Id);

            trainingPlanRepositoryMock.Verify(x => x.Delete(It.IsAny<TrainingPlan>()), Times.Once);
        }

        #region ARRANGE

        private TrainingPlanService trainingService;
        private List<Guid> ExampleGuidsExercise;
        private TrainingPlan ExampleTrainingPlan;
        private Exercise ExampleExercise;

        public TrainingPlanServiceTests() : base()
        {
            trainingPlanRepositoryMock = new Mock<ITrainingPlanRepository>();
            mapperMock = new Mock<IMapper>();
            trainingService = new TrainingPlanService(trainingPlanRepositoryMock.Object, exerciseRepositoryMock.Object, mapperMock.Object);
            ExampleGuidsExercise = new List<Guid>();
            ExampleExercise = FakeDataBase.GetInstance().Exercises.First();
            FakeDataBase.GetInstance().Exercises.ForEach(x => ExampleGuidsExercise.Add(x.Id));
            ExampleTrainingPlan = FakeDataBase.GetInstance().TrainingPlans.First();
        }

        #endregion
    }
}
