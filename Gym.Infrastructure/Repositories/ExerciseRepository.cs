using Gym.Core.Models;
using Gym.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        public List<Exercise> _exercises = FakeDataBase.GetInstance().Exercises;

        public void Add(Exercise exercise)
        {
            _exercises.Add(exercise);
        }

        public Exercise Get(Guid id)
        {
            return _exercises.SingleOrDefault(x => x.Id == id);
        }

        public Exercise Get(string name)
        {
            return _exercises.SingleOrDefault(x => x.Name == name);
        }

        public IEnumerable<Exercise> Get(BodyPart bodyPart)
        {
            return _exercises.Where(x => x.BodyPart == bodyPart);
        }

        public IEnumerable<Exercise> GetAll()
        {
            return _exercises;
        }

        public bool IsExist(Guid id)
        {
            return _exercises.Exists(x=>x.Id == id);
        }

        public bool IsExist(string name)
        {
            return _exercises.Exists(x => x.Name == name);
        }

        public bool IsExist(BodyPart bodyPart)
        {
            return _exercises.Exists(x => x.BodyPart == bodyPart);
        }

        public bool IsExist(Exercise exercise)
        {
            return _exercises.Exists(x => x == exercise);
        }

        public void Remove(Guid id)
        {
            var exercise = Get(id);

            _exercises.Remove(exercise);
        }

        public void Update(Exercise exercise)
        {
            throw new NotImplementedException();
        }
    }
}
