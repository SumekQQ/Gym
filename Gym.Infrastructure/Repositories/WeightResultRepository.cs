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
    public class WeightResultRepository : IWeightResultRepository
    {
        private readonly GymContext _context;

        public WeightResultRepository(GymContext context)
        {
            _context = context;
        }

        public async Task<WeightResult> Get(Guid id)
            => await _context.WeightResults.SingleAsync(x => x.Id == id);

        public async Task<IEnumerable<WeightResult>> Get(TrainingDay trainingDay)
            => await _context.WeightResults.Where(x => x.TrainingDay == trainingDay).ToListAsync();

        public async Task<IEnumerable<WeightResult>> Get(Exercise exercise)
            => await _context.WeightResults.Where(x => x.Exercise == exercise).ToListAsync();

        public async Task<IEnumerable<WeightResult>> GetAll()
            => await _context.WeightResults.ToListAsync();

        public async Task<bool> IsExist(TrainingDay trainingDay, Exercise exercise)
            => await _context.WeightResults.AnyAsync(x => x.TrainingDay.Id == trainingDay.Id && x.Exercise.Id == exercise.Id);

        public async Task Add(WeightResult result)
        {
            _context.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(WeightResult result)
        {
            _context.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task Update(WeightResult result)
        {
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
    }
}
