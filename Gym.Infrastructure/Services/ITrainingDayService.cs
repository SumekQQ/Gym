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

        IEnumerable<TrainingDayDTO> Get(TrainingPlan trainingPlan);

        void CreateNew(TrainingPlanDTO trainingPlan, string description);

        void Update(Guid id, TrainingPlanDTO trainingPlan, string description);

        void Delete(Guid id);
    }
}
