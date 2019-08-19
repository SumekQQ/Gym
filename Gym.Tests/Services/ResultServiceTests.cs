using Gym.Core.Models;
using Gym.Infrastructure.Repositories;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gym.Tests.Services
{
    public abstract class ResultServiceTests : ServiceTestsTemplate
    {
        [Fact]
        protected virtual async Task create_with_empty_or_null_exercise()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);
        }

        [Fact]
        protected virtual async Task create_with_empty_or_null_training_day()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);
        }

        [Fact]
        protected abstract Task create_if_exist();

        [Fact]
        protected abstract Task create_using_incorrect_category();

        [Fact]
        protected abstract Task create_using_negative_value();

        [Fact]
        protected virtual async Task create_correctly()
        {
            exerciseRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleExercise);
            trainingDayRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(ExampleTrainingDay);
        }

        [Fact]
        protected abstract Task get_single_if_not_exist();

        [Fact]
        protected abstract Task get_single_correctly();

        [Fact]
        protected abstract Task get_collection_if_not_exist();

        [Fact]
        protected abstract Task get_collection_correctly();

        [Fact]
        protected abstract Task update_if_not_exist();

        [Fact]
        protected abstract Task update_correctly();

        [Fact]
        protected abstract Task delete_if_not_exist();

        [Fact]
        protected abstract Task delete_correctly();

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
