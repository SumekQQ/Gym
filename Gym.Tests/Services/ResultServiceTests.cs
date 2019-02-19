using Gym.Core.Models;
using Gym.Infrastructure.Repositories;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Gym.Tests.Services
{
    public abstract class ResultServiceTests : ServiceTestsTemplate
    {
        [Fact]
        protected virtual void create_with_empty_or_null_exercise()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingDay);
        }

        [Fact]
        protected virtual void create_with_empty_or_null_training_day()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleExercise);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(value: null);
        }

        [Fact]
        protected abstract void create_if_exist();

        [Fact]
        protected abstract void create_using_incorrect_category();

        [Fact]
        protected abstract void create_using_empty_fields();

        [Fact]
        protected virtual void create_correctly()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleExercise);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(ExampleTrainingDay);
        }

        [Fact]
        protected abstract void get_single_if_not_exist();

        [Fact]
        protected abstract void get_single_correctly();

        [Fact]
        protected abstract void get_collection_if_not_exist();

        [Fact]
        protected abstract void get_collection_correctly();

        [Fact]
        protected abstract void update_if_not_exist();

        [Fact]
        protected abstract void update_correctly();

        [Fact]
        protected abstract void delete_if_not_exist();

        [Fact]
        protected abstract void delete_correctly();

        #region ARRANGE

        protected TrainingDay ExampleTrainingDay;
        protected Exercise ExampleExercise;

        protected ResultServiceTests()
        {
            ExampleTrainingDay = FakeDataBase.GetInstance().TrainingDay.First();
        }

        #endregion
    }
}
