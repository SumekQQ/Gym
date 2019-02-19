using AutoMapper;
using Gym.Core.Repositories;
using Moq;

namespace Gym.Tests.Services
{
    public abstract class ServiceTestsTemplate
    {
        protected Mock<ICardioResultRepository> cardioResultRepositoryMock;
        protected Mock<IWeightResultRepository> weightResultRepositoryMock;
        protected Mock<ITrainingDayRepository> trainingDayRepositoryMock;
        protected Mock<IExerciseRepository> exerciseRepositoryMock;
        protected Mock<ITrainingPlanRepository> trainingPlanRepositoryMock;
        protected Mock<IMapper> mapperMock;

        protected ServiceTestsTemplate()
        {
            cardioResultRepositoryMock = new Mock<ICardioResultRepository>();
            weightResultRepositoryMock = new Mock<IWeightResultRepository>();
            trainingDayRepositoryMock = new Mock<ITrainingDayRepository>();
            exerciseRepositoryMock = new Mock<IExerciseRepository>();
            trainingPlanRepositoryMock = new Mock<ITrainingPlanRepository>();
            mapperMock = new Mock<IMapper>();
        }
    }
}
