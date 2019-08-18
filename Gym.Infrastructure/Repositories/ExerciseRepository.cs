using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly GymContext _context;

        public ExerciseRepository(GymContext context)
        {
            _context = context;
        }

        public async Task<Exercise> Get(Guid id)
            => await _context.Exercises.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Exercise>> GetAll()
            => await _context.Exercises.ToListAsync();

        public async Task<bool> IsExist(string name)
            => await _context.Exercises.AnyAsync(x => x.Name == name);

        public async Task Add(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Exercise exercise)
        {
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
        }
    }
}
