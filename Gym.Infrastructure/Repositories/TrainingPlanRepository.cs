using Gym.Core.Models;
using Gym.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class TrainingPlanRepository : ITrainingPlanRepository
    {
        public List<TrainingPlan> _trainingPlans = FakeDataBase.GetInstance().TrainingPlans;

        public void Add(TrainingPlan trainingPlan)
        {
            _trainingPlans.Add(trainingPlan);
        }

        public TrainingPlan Get(Guid id)
        {
            return _trainingPlans.Single(x => x.Id == id);
        }

        public TrainingPlan Get(string name)
        {
            return _trainingPlans.Single(x => x.Name == name);
        }

        public IEnumerable<TrainingPlan> GetAll()
        {
            return _trainingPlans;
        }

        public bool IsExist(Guid id)
        {
            return _trainingPlans.Exists(x => x.Id == id);
        }

        public bool IsExist(string name)
        {
            return _trainingPlans.Exists(x => x.Name == name);
        }

        public bool IsExist(TrainingPlan trainingPlan)
        {
            return _trainingPlans.Exists(x => x == trainingPlan);
        }

        public void Remove(Guid id)
        {
            var trainingPlan = Get(id);

            _trainingPlans.Remove(trainingPlan);
        }

        public void Update(TrainingPlan trainingDay)
        {

        }
    }
}
