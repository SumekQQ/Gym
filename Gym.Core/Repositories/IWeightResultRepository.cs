using Gym.Core.Models;
using System;
using System.Collections.Generic;

namespace Gym.Core.Repositories
{
    public interface IWeightResultRepository : IRepository
    {
        WeightResult Get(Guid id);

        IEnumerable<WeightResult> Get(TrainingDay trainingDay);

        IEnumerable<WeightResult> Get(Exercise exercise);

        IEnumerable<WeightResult> GetAll();

        bool IsExist(TrainingDay trainingDay, Exercise exercise);

        bool IsExist(TrainingDay trainingDay);

        bool IsExist(Exercise exercise);

        bool IsExist(Guid id);

        void Add(WeightResult result);

        void Delete(WeightResult result);

        void Update(WeightResult result);
    }
}
