using Gym.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Core.Repositories
{
    public interface ITrainingDayRepository : IRepository
    {
        Task<TrainingDay> Get(Guid id);

        Task<IEnumerable<TrainingDay>> GetAll();

        Task<bool> IsExist(DateTime date);

        Task<bool> IsExist(TrainingPlan trainingPlan);

        Task Add(TrainingDay trainingDay);

        Task Delete(TrainingDay trainingDay);

        Task Update(TrainingDay trainingDay);
    }
}