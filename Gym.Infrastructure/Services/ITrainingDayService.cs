using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public interface ITrainingDayService : IService
    {
        TrainingDayDTO Get(DateTime date);

        TrainingDayDTO Get(Guid id);

        IEnumerable<TrainingDayDTO> GetCollection(Guid trainingPlanId);

        IEnumerable<TrainingDayDTO> GetAll();

        void CreateNew(Guid trainingPlanId, string description, string date);

        void Update(Guid id, Guid trainingPlanId, string description);

        void Delete(Guid id);
    }
}
