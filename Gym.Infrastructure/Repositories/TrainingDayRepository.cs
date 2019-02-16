using Gym.Core.Models;
using Gym.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class TrainingDayRepository : ITrainingDayRepository
    {
        private List<TrainingDay> _trainingDays = FakeDataBase.GetInstance().TrainingDay;

        public void Add(TrainingDay trainingDay)
        {
            _trainingDays.Add(trainingDay);
        }

        public TrainingDay Get(Guid id)
        {
            return _trainingDays.Single(x => x.Id == id);
        }

        public TrainingDay Get(DateTime date)
        {
            return _trainingDays.Single(x => x.Date == date);
        }

        public IEnumerable<TrainingDay> Get(TrainingPlan trainingPlan)
        {
            return _trainingDays.Where(x => x.TrainingPlan == trainingPlan);
        }

        public IEnumerable<TrainingDay> GetAll()
        {
            return _trainingDays;
        }

        public bool IsExist(DateTime date)
        {
            return _trainingDays.Exists(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
        }

        public bool IsExist(TrainingPlan trainingPlan)
        {
            return _trainingDays.Exists(x => x.TrainingPlan == trainingPlan);
        }

        public bool IsExist(TrainingDay trainingDay)
        {
            return _trainingDays.Exists(x => x == trainingDay);
        }

        public bool IsExist(Guid id)
        {
            return _trainingDays.Exists(x => x.Id == id);
        }

        public void Delete(TrainingDay trainingDay)
        {
            _trainingDays.Remove(trainingDay);
        }

        public void Update(TrainingDay trainingDay)
        {
            //ToDo
        }
    }
}
