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
    public class TrainingPlanServiceTests : ServiceTestsTemplate
    {
        [Fact]
        public void add_new_training_plan()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(FakeDataBase.GetInstance().Exercises.First());
            trainingService.CreateNew("NewPlan", ExampleGuidsExercise);

            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Once);
        }

        [Fact]
        public void add_new_training_plan_with_empty_fields()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(FakeDataBase.GetInstance().Exercises.First());
            var testCases = new List<Exception>()
            {
                Assert.Throws<Exception>(() =>  trainingService.CreateNew("", ExampleGuidsExercise )),
                Assert.Throws<Exception>(() =>  trainingService.CreateNew("NewPlan", new List<Guid>())),
                Assert.Throws<Exception>(() =>  trainingService.CreateNew("NewPlan", null))
            };

            Assert.Equal("Provided name is not correct.", testCases[0].Message);
            Assert.Equal("Finding data not exist or return null value", testCases[1].Message);
            Assert.Equal("Finding data not exist or return null value", testCases[2].Message);

            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public void add_new_training_using_exist_name()
        {
            trainingPlanRepositoryMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(true);

            var ex = Assert.Throws<Exception>(() => trainingService.CreateNew(ExampleTrainingPlan.Name, ExampleGuidsExercise));
            Assert.Equal($"{ErrorsCodes.ItemExist}", ex.Message);
            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public void add_new_when_some_exercise_return_null()
        {
            ExampleGuidsExercise.Add(Guid.NewGuid());

            var ex = Assert.Throws<Exception>(() => trainingService.CreateNew(ExampleTrainingPlan.Name, ExampleGuidsExercise));
            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingPlanRepositoryMock.Verify(x => x.Add(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public void get_single()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);
            var trainingPlan = trainingService.Get(ExampleTrainingPlan.Id);

            Assert.Equal(trainingPlan, mapperMock.Object.Map<TrainingPlan, TrainingPlanDTO>(ExampleTrainingPlan));
            trainingPlanRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void get_single_if_not_exist()
        {
            var ex = Assert.Throws<Exception>(() => trainingService.Get(ExampleTrainingPlan.Id));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingPlanRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void get_all()
        {
            trainingPlanRepositoryMock.Setup(x => x.GetAll()).Returns(FakeDataBase.GetInstance().TrainingPlans);

            var trainingPlans = trainingService.GetAll();

            Assert.Equal(trainingPlans, mapperMock.Object.Map<IEnumerable<TrainingPlan>, IEnumerable<TrainingPlanDTO>>(FakeDataBase.GetInstance().TrainingPlans));
            trainingPlanRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void get_all_if_not_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.GetAll()).Returns(value: null);
            var ex1 = Assert.Throws<Exception>(() => trainingService.GetAll());

            trainingPlanRepositoryMock.Setup(x => x.GetAll()).Returns(new List<TrainingPlan>());
            var ex2 = Assert.Throws<Exception>(() => trainingService.GetAll());

            Assert.Equal("Finding data not exist or return null value", ex1.Message);
            Assert.Equal("Finding data not exist or return null value", ex2.Message);
            trainingPlanRepositoryMock.Verify(x => x.GetAll(), Times.AtLeast(2));
        }

        [Fact]
        public void update_if_not_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => trainingService.Update(ExampleTrainingPlan.Id, ExampleTrainingPlan.Name, ExampleGuidsExercise));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingPlanRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public void update_using_empty_name()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(FakeDataBase.GetInstance().Exercises.First());
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);

            var ex = Assert.Throws<Exception>(() => trainingService.Update(ExampleTrainingPlan.Id, "", ExampleGuidsExercise));

            Assert.Equal("Provided name is not correct.", ex.Message);
            trainingPlanRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public void update_using_incorrect_exercise_list()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);
            ExampleGuidsExercise.Add(Guid.NewGuid());

            var ex = Assert.Throws<Exception>(() => trainingService.Update(ExampleTrainingPlan.Id, ExampleTrainingPlan.Name, ExampleGuidsExercise));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingPlanRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public void update_correctly()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(FakeDataBase.GetInstance().Exercises.First());
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);
            ExampleGuidsExercise.RemoveAt(4);

            trainingService.Update(ExampleTrainingPlan.Id, "NewName", ExampleGuidsExercise);

            trainingPlanRepositoryMock.Verify(x => x.Update(It.IsAny<TrainingPlan>()), Times.Once);
        }

        [Fact]
        public void delete_if_not_exist()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => trainingService.Delete(Guid.NewGuid()));

            Assert.Equal("Finding data not exist or return null value", ex.Message);
            trainingPlanRepositoryMock.Verify(x => x.Delete(It.IsAny<TrainingPlan>()), Times.Never);
        }

        [Fact]
        public void delete_correctly()
        {
            trainingPlanRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingPlan);

            trainingService.Delete(ExampleTrainingPlan.Id);

            trainingPlanRepositoryMock.Verify(x => x.Delete(It.IsAny<TrainingPlan>()), Times.Once);
        }

        #region ARRANGE

        private TrainingPlanService trainingService;
        private List<Guid> ExampleGuidsExercise;
        private TrainingPlan ExampleTrainingPlan;

        public TrainingPlanServiceTests() : base()
        {
            trainingPlanRepositoryMock = new Mock<ITrainingPlanRepository>();
            mapperMock = new Mock<IMapper>();
            trainingService = new TrainingPlanService(trainingPlanRepositoryMock.Object, exerciseRepositoryMock.Object,trainingPlanExerciseRepository.Object, mapperMock.Object);
            ExampleGuidsExercise = new List<Guid>();
            FakeDataBase.GetInstance().Exercises.ForEach(x => ExampleGuidsExercise.Add(x.Id));
            ExampleTrainingPlan = FakeDataBase.GetInstance().TrainingPlans.First();
        }

        #endregion
    }
}
