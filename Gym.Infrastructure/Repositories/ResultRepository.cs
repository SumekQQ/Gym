using Gym.Core.Models;
using Gym.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private List<Result> _results = FakeDataBase.GetInstance().Results;

        public void Add(Result result)
        {
            _results.Add(result);
        }

        public Result Get(Guid id)
        {
            return _results.Single(x => x.Id == id);
        }

        public IEnumerable<Result> Get(TrainingDay trainingDay)
        {
            return _results.Where(x => x.TrainingDay == trainingDay);
        }

        public IEnumerable<Result> Get(Exercise exercise)
        {
            return _results.Where(x => x.Exercise == exercise);
        }

        public IEnumerable<Result> GetAll()
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

        public void Remove(Guid id)
        {
            var result = Get(id);

            _results.Remove(result);
        }

        public void Update(Result result)
        {

        }
    }
}
