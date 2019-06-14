using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class TrainingPlanRepository : ITrainingPlanRepository
    {
        private readonly GymContext _context;

        public TrainingPlanRepository(GymContext context)
        {
            _context = context;
        }

        public void Add(TrainingPlan trainingPlan)
        {
            _context.TrainingPlans.Add(trainingPlan);
            _context.SaveChanges();
        }

        public TrainingPlan Get(Guid id)
        {
            return _context.TrainingPlans.Include(x => x.ExerciseIds).ThenInclude(x => x.Exercise).Single(x => x.Id == id);
        }

        public IEnumerable<TrainingPlan> GetAll()
           => _context.TrainingPlans.Include(x => x.ExerciseIds).ThenInclude(x => x.Exercise);

        public bool IsExist(Guid id)
            => _context.TrainingPlans.Any(x => x.Id == id);

        public bool IsExist(string name)
            => _context.TrainingPlans.Any(x => x.Name == name);

        public bool IsExist(TrainingPlan trainingPlan)
            => _context.TrainingPlans.Any(x => x == trainingPlan);

        public void Delete(TrainingPlan trainingPlan)
        {
            _context.TrainingPlans.Remove(trainingPlan);
            _context.SaveChanges();
        }

        public void Update(TrainingPlan trainingPlan)
        {
            _context.TrainingPlans.Update(trainingPlan);
            _context.SaveChanges();
        }
    }
}
