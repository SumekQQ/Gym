using Gym.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Core.Repositories
{
    public interface IExerciseRepository : IRepository
    {
        Task<Exercise> Get(Guid id);

        Task<IEnumerable<Exercise>> GetAll();

        Task<bool> IsExist(string name);

        Task Add(Exercise exercise);

        Task Delete(Exercise exercise);

        Task Update(Exercise exercise);
    }
}
