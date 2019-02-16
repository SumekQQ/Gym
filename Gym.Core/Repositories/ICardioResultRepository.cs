using Gym.Core.Models;
using System;
using System.Collections.Generic;

namespace Gym.Core.Repositories
{
    public interface ICardioResultRepository : IRepository
    {
        CardioResult Get(Guid id);

        IEnumerable<CardioResult> Get(TrainingDay trainingDay);

        IEnumerable<CardioResult> Get(Exercise exercise);

        IEnumerable<CardioResult> GetAll();

        bool IsExist(TrainingDay trainingDay, Exercise exercise);

        bool IsExist(TrainingDay trainingDay);

        bool IsExist(Exercise exercise);

        bool IsExist(Guid id);

        void Add(CardioResult result);

        void Delete(CardioResult result);

        void Update(CardioResult result);
    }
}
