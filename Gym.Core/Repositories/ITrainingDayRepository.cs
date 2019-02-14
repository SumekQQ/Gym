using Gym.Core.Models;
using System;
using System.Collections.Generic;

namespace Gym.Core.Repositories
{
    public interface ITrainingDayRepository : IRepository
    {
        TrainingDay Get(Guid id);

        TrainingDay Get(DateTime date);

        IEnumerable<TrainingDay> Get(TrainingPlan trainingPlan);

        bool IsExist(Guid id);

        bool IsExist(DateTime date);

        bool IsExist(TrainingPlan trainingPlan);

        bool IsExist(TrainingDay trainingDay);

        IEnumerable<TrainingDay> GetAll();

        void Add(TrainingDay trainingDay);

        void Remove(Guid id);

        void Update(TrainingDay trainingDay);
    }
}