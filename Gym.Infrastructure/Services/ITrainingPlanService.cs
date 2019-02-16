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

        void CreateNew(string name, IEnumerable<Guid> exercisesId);

        void Update(Guid id, string name, IEnumerable<Guid> exercisesId);

        void Delete(Guid id);
    }
}
