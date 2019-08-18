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
    public class TrainingDayRepository : ITrainingDayRepository
    {
        private readonly GymContext _context;

        public TrainingDayRepository(GymContext context)
        {
            _context = context;
        }

        public async Task<TrainingDay> Get(Guid id)
            => await _context.TrainingDays.Include(x => x.TrainingPlan).SingleAsync(x => x.Id == id);

        public async Task<IEnumerable<TrainingDay>> GetAll()
            => await _context.TrainingDays.Include(x => x.TrainingPlan).ToListAsync();

        public async Task<bool> IsExist(DateTime date)
            => await _context.TrainingDays.AnyAsync(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);

        public async Task<bool> IsExist(TrainingPlan trainingPlan)
            => await _context.TrainingDays.AnyAsync(x => x.TrainingPlan == trainingPlan);

        public async Task Delete(TrainingDay trainingDay)
        {
            _context.TrainingDays.Remove(trainingDay);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TrainingDay trainingDay)
        {
            _context.TrainingDays.Update(trainingDay);
            await _context.SaveChangesAsync();
        }

        public async Task Add(TrainingDay trainingDay)
        {
            _context.TrainingDays.Add(trainingDay);
            await _context.SaveChangesAsync();
        }
    }
}
