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
    public class CardioResultServiceTests : ResultServiceTests
    {
        protected override void create_correctly()
        {
            base.create_correctly();
            cardioResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<Guid>())).Returns(false);

            resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 100, "50:70:25");

            cardioResultRepositoryMock.Verify(x => x.Add(It.IsAny<CardioResult>()), Times.Once);
        }

        protected override void create_if_exist()
        {
            base.create_correctly();
            cardioResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingDay>(), It.IsAny<Exercise>())).Returns(true);

            Exception ex = Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 100, "50:70:25"));

            Assert.Equal($"{ErrorsCodes.ItemExist}", ex.Message);
            cardioResultRepositoryMock.Verify(x => x.Add(It.IsAny<CardioResult>()), Times.Never);
        }

        protected override void create_with_empty_or_null_exercise()
        {
            base.create_with_empty_or_null_exercise();
            cardioResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<Guid>())).Returns(false);

            Exception ex = Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 100, "50:70:25"));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            cardioResultRepositoryMock.Verify(x => x.Add(It.IsAny<CardioResult>()), Times.Never);
        }

        protected override void create_using_incorrect_category()
        {
            var incorrectExercise = FakeDataBase.GetInstance().Exercises.First(x => x.Category != Category.Cardio);
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(incorrectExercise);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingDay);
            cardioResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<Guid>())).Returns(false);

            Exception ex = Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 100, "50:70:25"));

            Assert.Equal($"Can not create {incorrectExercise.Category.ToString()} as cardio result", ex.Message);
            cardioResultRepositoryMock.Verify(x => x.Add(It.IsAny<CardioResult>()), Times.Never);
        }

        protected override void create_using_empty_fields()
        {
            base.create_correctly();
            cardioResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<TrainingDay>(), It.IsAny<Exercise>())).Returns(false);

            var exceptions = new List<Exception>()
            {
                Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, -5, "50:70:25")),
                Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 100, "")),
            };

            Assert.Equal("Exercise distance can not be less than 0.", exceptions[0].Message);
            Assert.Equal("Training time should contain only hours, mins and seconds", exceptions[1].Message);
            cardioResultRepositoryMock.Verify(x => x.Add(It.IsAny<CardioResult>()), Times.Never);
        }

        protected override void create_with_empty_or_null_training_day()
        {
            base.create_with_empty_or_null_training_day();
            cardioResultRepositoryMock.Setup(x => x.IsExist(It.IsAny<Guid>())).Returns(false);

            Exception ex = Assert.Throws<Exception>(() => resultService.CreateNew(ExampleTrainingDay.Id, ExampleExercise.Id, 100, "50:70:25"));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            cardioResultRepositoryMock.Verify(x => x.Add(It.IsAny<CardioResult>()), Times.Never);
        }

        protected override void delete_correctly()
        {
            cardioResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleResult);

            resultService.Delete(Guid.NewGuid());

            cardioResultRepositoryMock.Verify(x => x.Delete(It.IsAny<CardioResult>()), Times.Once);
        }

        protected override void delete_if_not_exist()
        {
            cardioResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => resultService.Delete(Guid.NewGuid()));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            cardioResultRepositoryMock.Verify(x => x.Delete(It.IsAny<CardioResult>()), Times.Never);
        }

        protected override void get_collection_correctly()
        {
            cardioResultRepositoryMock.Setup(x => x.Get(It.IsAny<Exercise>())).Returns(FakeDataBase.GetInstance().CardioResults);

            resultService.Get(It.IsAny<Exercise>());

            cardioResultRepositoryMock.Verify(x => x.Get(It.IsAny<Exercise>()), Times.Once);
        }

        protected override void get_collection_if_not_exist()
        {
            cardioResultRepositoryMock.Setup(x => x.Get(It.IsAny<Exercise>())).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => resultService.Get(It.IsAny<Exercise>()));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
        }

        protected override void get_single_correctly()
        {
            cardioResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleResult);

            resultService.Get(It.IsAny<Guid>());

            cardioResultRepositoryMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        protected override void get_single_if_not_exist()
        {
            cardioResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            var ex = Assert.Throws<Exception>(() => resultService.Get(It.IsAny<Guid>()));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
        }

        protected override void update_correctly()
        {
            cardioResultRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleResult);

            resultService.Update(ExampleResult.Id, 5, "10:12:13");

            cardioResultRepositoryMock.Verify(x => x.Update(It.IsAny<CardioResult>()), Times.Once);
        }

        protected override void update_if_not_exist()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);

            Exception ex = Assert.Throws<Exception>(() => resultService.Update(ExampleResult.Id, 5, "10:12:13"));

            Assert.Equal($"Finding data not exist or return null value", ex.Message);
            cardioResultRepositoryMock.Verify(x => x.Update(It.IsAny<CardioResult>()), Times.Never);
        }

        #region ARRANGE

        private CardioResultService resultService;
        private CardioResult ExampleResult;

        public CardioResultServiceTests()
        {
            resultService = new CardioResultService(cardioResultRepositoryMock.Object, trainingDayRepositoryMock.Object, exerciseRepositoryMock.Object, mapperMock.Object);
            ExampleExercise = FakeDataBase.GetInstance().Exercises.First(x => x.Category == Category.Cardio);
            ExampleResult = FakeDataBase.GetInstance().CardioResults.First();
        }

        #endregion
    }
}
