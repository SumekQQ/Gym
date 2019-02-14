using Gym.Core.Models;
using System;
using System.Collections.Generic;

namespace Gym.Core.Repositories
{
    public interface IResultRepository : IRepository
    {
        Result Get(Guid id);

        IEnumerable<Result> Get(TrainingDay trainingDay);

        IEnumerable<Result> Get(Exercise exercise);

        IEnumerable<Result> GetAll();

        bool IsExist(TrainingDay trainingDay, Exercise exercise);

        bool IsExist(TrainingDay trainingDay);

        bool IsExist(Exercise exercise);

        bool IsExist(Guid id);

        void Add(Result result);

        void Remove(Guid id);

        void Update(Result result);
    }
}
