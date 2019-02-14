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
    }
}
