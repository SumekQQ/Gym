using Gym.Core.Models;
using System;
using System.Collections.Generic;

namespace Gym.Core.Repositories
{
    public interface IExerciseRepository : IRepository
    {
        Exercise Get(Guid id);

        Exercise Get(string name);

        IEnumerable<Exercise> Get(Category category);

        IEnumerable<Exercise> GetAll();

        bool IsExist(Guid id);

        bool IsExist(string name);

        bool IsExist(Category category);

        bool IsExist(Exercise exercise);

        void Add(Exercise exercise);

        void Delete(Exercise exercise);

        void Update(Exercise exercise);
    }
}
