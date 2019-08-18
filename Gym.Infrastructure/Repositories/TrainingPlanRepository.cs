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
    public class TrainingPlanRepository : ITrainingPlanRepository
    {
        private readonly GymContext _context;

        public TrainingPlanRepository(GymContext context)
        {
            _context = context;
        }

        public async Task<TrainingPlan> Get(Guid id)
           => await _context.TrainingPlans.Include(x => x.ExerciseIds).ThenInclude(x => x.Exercise).SingleAsync(x => x.Id == id);

        public async Task<IEnumerable<TrainingPlan>> GetAll()
           => await _context.TrainingPlans.Include(x => x.ExerciseIds).ThenInclude(x => x.Exercise).ToListAsync();

        public async Task<bool> IsExist(string name)
            => await _context.TrainingPlans.AnyAsync(x => x.Name == name);

        public async Task Add(TrainingPlan trainingPlan)
        {
            _context.TrainingPlans.Add(trainingPlan);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TrainingPlan trainingPlan)
        {
            _context.TrainingPlans.Remove(trainingPlan);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TrainingPlan trainingPlan)
        {
            _context.TrainingPlans.Update(trainingPlan);
            await _context.SaveChangesAsync();
        }
    }
}
