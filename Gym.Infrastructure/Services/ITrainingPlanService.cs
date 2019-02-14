using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public interface ITrainingPlanService : IService
    {
        TrainingPlanDTO Get(Guid id);

        TrainingPlanDTO Get(string name);

        IEnumerable<TrainingPlanDTO> GetAll();

        void CreateNew(string name, IEnumerable<ExerciseDTO> exercises);
    }
}
