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
            => _trainingPlans.Add(trainingPlan);

        public TrainingPlan Get(Guid id)
            => _trainingPlans.Single(x => x.Id == id);

        public TrainingPlan Get(string name)
            => _trainingPlans.Single(x => x.Name == name);

        public IEnumerable<TrainingPlan> GetAll()
            => _trainingPlans;

        public bool IsExist(Guid id)
            => _trainingPlans.Exists(x => x.Id == id);

        public bool IsExist(string name)
            => _trainingPlans.Exists(x => x.Name == name);

        public bool IsExist(TrainingPlan trainingPlan)
            => _trainingPlans.Exists(x => x == trainingPlan);

        public void Delete(TrainingPlan trainingPlan)
            => _trainingPlans.Remove(trainingPlan);

        public void Update(TrainingPlan trainingPlan)
        {
            //DoTo
        }
    }
}
