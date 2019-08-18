using Gym.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Core.Repositories
{
    public interface ITrainingPlanRepository : IRepository
    {
        Task<TrainingPlan> Get(Guid id);

        Task<IEnumerable<TrainingPlan>> GetAll();

        Task<bool> IsExist(string name);

        Task Add(TrainingPlan trainingDay);

        Task Delete(TrainingPlan trainingPlan);

        Task Update(TrainingPlan trainingPlan);
    }
}
