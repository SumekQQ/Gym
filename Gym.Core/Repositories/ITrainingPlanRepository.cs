using Gym.Core.Models;
using System;
using System.Collections.Generic;

namespace Gym.Core.Repositories
{
    public interface ITrainingPlanRepository : IRepository
    {
        TrainingPlan Get(Guid id);

        TrainingPlan Get(string name);

        IEnumerable<TrainingPlan> GetAll();

        bool IsExist(Guid id);

        bool IsExist(string name);

        bool IsExist(TrainingPlan trainingPlan);

        void Add(TrainingPlan trainingDay);

        void Delete(TrainingPlan trainingPlan);

        void Update(TrainingPlan trainingPlan);
    }
}
