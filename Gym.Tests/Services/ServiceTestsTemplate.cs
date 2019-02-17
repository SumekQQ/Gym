using AutoMapper;
using Gym.Core.Repositories;
using Gym.Infrastructure.Repositories;
using Moq;

namespace Gym.Tests.Services
{
    public abstract class ServiceTestsTemplate
    {
     //   protected Mock<IResultRepository> resultRepositorMock;
        protected Mock<ITrainingDayRepository> trainingDayRepositoryMock;
        protected Mock<IExerciseRepository> exerciseRepositoryMock;
        protected Mock<ITrainingPlanRepository> trainingPlanRepositoryMock;
        protected Mock<IMapper> mapperMock;

        protected ServiceTestsTemplate()
        {
           // resultRepositorMock = new Mock<IResultRepository>();
            trainingDayRepositoryMock = new Mock<ITrainingDayRepository>();
            exerciseRepositoryMock = new Mock<IExerciseRepository>();
            trainingPlanRepositoryMock = new Mock<ITrainingPlanRepository>();
            mapperMock = new Mock<IMapper>();
        }
    }
}
