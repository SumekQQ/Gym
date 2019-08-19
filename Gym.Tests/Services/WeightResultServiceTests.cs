using AutoMapper;
using Gym.Core.Exceptions;
using Gym.Core.Models;
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
    public class WeightResultServiceTests : ResultServiceTests
    {
        protected override async Task get_collection_correctly()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Exercise>())).ReturnsAsync(FakeDataBase.GetInstance().WeightResults);

            await resultService.Get(It.IsAny<Exercise>());

            weightResultRepositoryMock.Verify(x => x.Get(It.IsAny<Exercise>()), Times.Once);
        }

        protected override async Task get_collection_if_not_exist()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Exercise>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => resultService.Get(It.IsAny<Exercise>()));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            weightResultRepositoryMock.Verify(x => x.Get(It.IsAny<Exercise>()), Times.Once);
        }

        protected override async Task get_single_correctly()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleResult);

            await resultService.Get(It.IsAny<Guid>());

            weightResultRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        protected override async Task get_single_if_not_exist()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => resultService.Get(It.IsAny<Guid>()));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            weightResultRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        protected override async Task create_correctly()
        {
            await base.create_correctly();
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);

            await resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9);

            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Once);
        }

        protected override async Task create_if_exist()
        {
            await base.create_correctly();
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingDay>(), It.IsAny<Exercise>())).ReturnsAsync(true);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9));

            Assert.Equal(ErrorsCodes.ItemExist, ex.Code);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override async Task create_with_empty_or_null_exercise()
        {
            await base.create_with_empty_or_null_exercise();
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override async Task create_using_incorrect_category()
        {
            var incorrectExercise = FakeDataBase.GetInstance().Exercises.Single(x => x.Category == Category.Cardio);
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(incorrectExercise);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingDay>(), It.IsAny<Exercise>())).ReturnsAsync(false);

            var ex = await Assert.ThrowsAsync<DomainException>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9));

            Assert.Equal(ErrorsCodes.IncorrectCategory, ex.Code);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override async Task create_using_negative_value()
        {
            await base.create_correctly();
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingDay>(), It.IsAny<Exercise>())).ReturnsAsync(false);

            var exceptions = new List<DomainException>()
            {
               await Assert.ThrowsAsync<DomainException>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 0,5,5)),
               await Assert.ThrowsAsync<DomainException>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5,-1,5)),
               await Assert.ThrowsAsync<DomainException>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5,5,-1)),
            };

            foreach (var ex in exceptions)
                Assert.Equal(ErrorsCodes.NeagtiveValue, ex.Code);

            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override async Task create_with_empty_or_null_training_day()
        {
            await base.create_with_empty_or_null_training_day();
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingDay>(), It.IsAny<Exercise>())).ReturnsAsync(false);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override async Task delete_correctly()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleResult);

            await resultService.Delete(Guid.NewGuid());

            weightResultRepositoryMock.Verify(x => x.Delete(It.IsAny<WeightResult>()), Times.Once);
        }

        protected override async Task delete_if_not_exist()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex =  await Assert.ThrowsAsync<ServiceException>(() => resultService.Delete(Guid.NewGuid()));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            weightResultRepositoryMock.Verify(x => x.Delete(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override async Task update_correctly()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleResult);

            await resultService.Update(ExampleResult.Id, 5, 5, 5);

            weightResultRepositoryMock.Verify(x => x.Update(It.IsAny<WeightResult>()), Times.Once);
        }

        protected override async Task update_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var ex = await Assert.ThrowsAsync<ServiceException>(() => resultService.Update(ExampleResult.Id, 5, 5, 5));

            Assert.Equal(ErrorsCodes.ItemNotFound, ex.Code);
            weightResultRepositoryMock.Verify(x => x.Update(It.IsAny<WeightResult>()), Times.Never);
        }

        #region ARRANGE

        private WeightResultService resultService;
        private WeightResult ExampleResult;

        public WeightResultServiceTests()
        {
            resultService = new WeightResultService(weightResultRepositoryMock.Object, trainingDayRepositoryMock.Object, exerciseRepositoryMock.Object, mapperMock.Object);
            ExampleExercise = FakeDataBase.GetInstance().Exercises.First(x => x.Category != Category.Cardio);
            ExampleResult = FakeDataBase.GetInstance().WeightResults.First();
        }

        #endregion
    }
}
