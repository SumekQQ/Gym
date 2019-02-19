using AutoMapper;
using Gym.Core.Models;
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
    public class WeightResultServiceTests : ResultServiceTests
    {
        protected override void create_correctly()
        {
            base.create_correctly();
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<Guid>())).Returns(false);

            resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9);

            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Once);
        }

        protected override void create_if_exist()
        {
            base.create_correctly();
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingDay>(), It.IsAny<Exercise>())).Returns(true);

            Exception ex = Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9));

            Assert.Equal($"{ErrorsCodes.ItemExist}", ex.Message);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override void create_with_empty_or_null_exercise()
        {
            base.create_with_empty_or_null_exercise();
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<Guid>())).Returns(false);

            Exception ex = Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override void create_using_incorrect_category()
        {
            var incorrectExercise = FakeDataBase.GetInstance().Exercises.Single(x => x.Category == Category.Cardio);
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(incorrectExercise);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingDay);
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<Guid>())).Returns(false);

            Exception ex = Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9));

            Assert.Equal($"Can not create as cardio result {incorrectExercise.Category.ToString()}.", ex.Message);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override void create_using_empty_fields()
        {
            base.create_correctly();
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingDay>(), It.IsAny<Exercise>())).Returns(false);

            var exceptions = new List<Exception>()
            {
                Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 0,5,5)),
                Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5,-1,5)),
                Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5,5,-1)),
            };

            Assert.Equal("Provided amount of series is not correct.", exceptions[0].Message);
            Assert.Equal("Provided weights is not correct.", exceptions[1].Message);
            Assert.Equal("Provided amount of reps is not correct.", exceptions[2].Message);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override void create_with_empty_or_null_training_day()
        {
            base.create_with_empty_or_null_training_day();
            weightResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<Guid>())).Returns(false);

            Exception ex = Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 5, 8, 9));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            weightResultRepositoryMock.Verify(x => x.Add(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override void delete_correctly()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleResult);

            resultService.Delete(Guid.NewGuid());

            weightResultRepositoryMock.Verify(x => x.Delete(It.IsAny<WeightResult>()), Times.Once);
        }

        protected override void delete_if_not_exist()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => resultService.Delete(Guid.NewGuid()));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            weightResultRepositoryMock.Verify(x => x.Delete(It.IsAny<WeightResult>()), Times.Never);
        }

        protected override void get_collection_correctly()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Exercise>())).Returns(FakeDataBase.GetInstance().WeightResults);

            resultService.Get(It.IsAny<Exercise>());

            weightResultRepositoryMock.Verify(x => x.Get(It.IsAny<Exercise>()), Times.Once);
        }

        protected override void get_collection_if_not_exist()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Exercise>())).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => resultService.Get(It.IsAny<Exercise>()));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
        }

        protected override void get_single_correctly()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleResult);

            resultService.Get(It.IsAny<Guid>());

            weightResultRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        protected override void get_single_if_not_exist()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => resultService.Get(It.IsAny<Guid>()));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
        }

        protected override void update_correctly()
        {
            weightResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleResult);

            resultService.Update(ExampleResult.Id, 5, 5, 5);

            weightResultRepositoryMock.Verify(x => x.Update(It.IsAny<WeightResult>()), Times.Once);
        }

        protected override void update_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            Exception ex = Assert.Throws<Exception>(() => resultService.Update(ExampleResult.Id, 5, 5, 5));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
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
