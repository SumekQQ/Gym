using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class TrainingDayRepository : ITrainingDayRepository
    {
        private readonly GymContext _context;

        public TrainingDayRepository(GymContext context)
        {
            _context = context;
        }

        public TrainingDay Get(Guid id)
        {
            return _context.TrainingDays.Include(x=>x.TrainingPlan).Single(x => x.Id == id);
        }

        public TrainingDay Get(DateTime date)
        {
            return _context.TrainingDays.Include(x => x.TrainingPlan).Single(x => x.Date == date);
        }

        public IEnumerable<TrainingDay> Get(TrainingPlan trainingPlan)
        {
            return _context.TrainingDays.Include(x => x.TrainingPlan).Where(x => x.TrainingPlan == trainingPlan);
        }

        public IEnumerable<TrainingDay> GetAll()
        {
            return _context.TrainingDays.Include(x=>x.TrainingPlan);
        }

        public bool IsExist(DateTime date)
        {
            return _context.TrainingDays.Any(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
        }

        public bool IsExist(TrainingPlan trainingPlan)
        {
            return _context.TrainingDays.Any(x => x.TrainingPlan == trainingPlan);
        }

        public bool IsExist(TrainingDay trainingDay)
        {
            return _context.TrainingDays.Any(x => x == trainingDay);
        }

        public bool IsExist(Guid id)
        {
            return _context.TrainingDays.Any(x => x.Id == id);
        }

        public void Delete(TrainingDay trainingDay)
        {
            _context.TrainingDays.Remove(trainingDay);
            _context.SaveChanges();

        }

        public void Update(TrainingDay trainingDay)
        {
            _context.TrainingDays.Update(trainingDay);
            _context.SaveChanges();
        }

        public void Add(TrainingDay trainingDay)
        {
            _context.TrainingDays.Add(trainingDay);
            _context.SaveChanges();
        }
    }
}
