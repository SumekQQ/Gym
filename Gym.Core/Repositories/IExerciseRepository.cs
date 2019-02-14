using Gym.Core.Models;
using System;
using System.Collections.Generic;

namespace Gym.Core.Repositories
{
    public interface IExerciseRepository : IRepository
    {
        Exercise Get(Guid id);

        Exercise Get(string name);

        IEnumerable<Exercise> Get(BodyPart bodyPart);

        IEnumerable<Exercise> GetAll();

        bool IsExist(Guid id);

        bool IsExist(string name);

        bool IsExist(BodyPart bodyPart);

        bool IsExist(Exercise exercise);

        void Add(Exercise exercise);

        void Remove(Guid id);

        void Update(Exercise exercise);
    }
}
