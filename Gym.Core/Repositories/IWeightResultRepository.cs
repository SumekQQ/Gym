using Gym.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Core.Repositories
{
    public interface IWeightResultRepository : IRepository
    {
        Task<WeightResult> Get(Guid id);

        Task<IEnumerable<WeightResult>> Get(TrainingDay trainingDay);

        Task<IEnumerable<WeightResult>> Get(Exercise exercise);

        Task<IEnumerable<WeightResult>> GetAll();

        Task<bool> IsExist(TrainingDay trainingDay, Exercise exercise);

        Task Add(WeightResult result);

        Task Delete(WeightResult result);

        Task Update(WeightResult result);
    }
}
