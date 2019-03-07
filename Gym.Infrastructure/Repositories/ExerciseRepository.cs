using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly GymContext _context;

        public ExerciseRepository(GymContext context)
        {
            _context = context;
        }

        public void Add(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            _context.SaveChanges();
        }

        public Exercise Get(Guid id)
            => _context.Exercises.SingleOrDefault(x => x.Id == id);

        public Exercise Get(string name)
            => _context.Exercises.SingleOrDefault(x => x.Name == name);

        public IEnumerable<Exercise> Get(Category category)
            => _context.Exercises.Where(x => x.Category == category);

        public IEnumerable<Exercise> GetAll()
            => _context.Exercises;

        public bool IsExist(Guid id)
            => _context.Exercises.Any(x => x.Id == id);

        public bool IsExist(string name)
            => _context.Exercises.Any(x => x.Name == name);

        public bool IsExist(Category category)
            => _context.Exercises.Any(x => x.Category == category);

        public bool IsExist(Exercise exercise)
            => _context.Exercises.Any(x => x == exercise);

        public void Delete(Exercise exercise)
        {
            _context.Exercises.Remove(exercise);
            _context.SaveChanges();
        }

        public void Update(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            _context.SaveChanges();
        }
    }
}
