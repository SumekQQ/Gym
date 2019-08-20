using Gym.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Core.Repositories
{
    public interface ICardioResultRepository : IRepository
    {
        Task<CardioResult> Get(Guid id);

        Task<IEnumerable<CardioResult>> Get(TrainingDay trainingDay);

        Task<IEnumerable<CardioResult>> Get(Exercise exercise);

        Task<IEnumerable<CardioResult>> GetAll();

        Task<bool> IsExist(TrainingDay trainingDay, Exercise exercise);

        Task Add(CardioResult result);

        Task Delete(CardioResult result);

        Task Update(CardioResult result);
    }
}
