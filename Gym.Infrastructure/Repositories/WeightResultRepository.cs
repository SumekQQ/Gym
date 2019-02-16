using Gym.Core.Models;
using Gym.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class WeightResultRepository : IWeightResultRepository
    {
        private List<WeightResult> _results = FakeDataBase.GetInstance().WeightResults;

        public void Add(WeightResult result)
        {
            _results.Add(result);
        }

        public WeightResult Get(Guid id)
        {
            return _results.Single(x => x.Id == id);
        }

        public IEnumerable<WeightResult> Get(TrainingDay trainingDay)
        {
            return _results.Where(x => x.TrainingDay == trainingDay);
        }

        public IEnumerable<WeightResult> Get(Exercise exercise)
        {
            return _results.Where(x => x.Exercise == exercise);
        }

        public IEnumerable<WeightResult> GetAll()
        {
            return _results;
        }

        public bool IsExist(TrainingDay trainingDay, Exercise exercise)
        {
            return IsExist(trainingDay) && IsExist(exercise);
        }

        public bool IsExist(TrainingDay trainingDay)
        {
            return _results.Exists(x => x.TrainingDay == trainingDay);
        }

        public bool IsExist(Exercise exercise)
        {
            return _results.Exists(x => x.Exercise == exercise);
        }

        public bool IsExist(Guid id)
        {
            return _results.Exists(x => x.Id == id);
        }

        public void Delete(WeightResult result)
        {
            _results.Remove(result);
        }

        public void Update(WeightResult result)
        {

        }
    }
}
