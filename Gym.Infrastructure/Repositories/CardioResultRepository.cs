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
    public class CardioResultRepository : ICardioResultRepository
    {
        private readonly GymContext _context;

        public CardioResultRepository(GymContext context)
        {
            _context = context;
        }

        public async Task<CardioResult> Get(Guid id)
            => await _context.CardioResults.SingleAsync(x => x.Id == id);

        public async Task<IEnumerable<CardioResult>> Get(TrainingDay trainingDay)
            => await _context.CardioResults.Where(x => x.TrainingDay == trainingDay).ToListAsync();

        public async Task<IEnumerable<CardioResult>> Get(Exercise exercise)
            => await _context.CardioResults.Where(x => x.Exercise == exercise).ToListAsync();

        public async Task<IEnumerable<CardioResult>> GetAll()
            => await _context.CardioResults.ToListAsync();

        public async Task<bool> IsExist(TrainingDay trainingDay, Exercise exercise)
            => await _context.CardioResults.AnyAsync(x => x.TrainingDay.Id == trainingDay.Id && x.Exercise.Id == exercise.Id);

        public async Task Add(CardioResult result)
        {
            _context.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CardioResult result)
        {
            _context.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CardioResult result)
        {
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
    }
}
