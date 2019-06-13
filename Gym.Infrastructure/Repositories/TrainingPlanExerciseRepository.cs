using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.EF;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public class TrainingPlanExerciseRepository : ITrainingPlanExerciseRepository
    {
     /*   private readonly GymContext _context;

        public TrainingPlanExerciseRepository(GymContext context)
        {
            _context = context;
        }

        public IEnumerable<TrainingPlanExercise> Get(TrainingPlan trainingPlan)
            => _context.TrainingPlanExercises.Where(x => x.TrainigPlanId == trainingPlan.Id).ToList();

        public void Add(TrainingPlanExercise trainingPlanExercise)
        {
            _context.TrainingPlanExercises.Add(trainingPlanExercise);
            _context.SaveChanges();
        }

        public void Add(List<TrainingPlanExercise> trainingPlanExercises)
        {
            _context.TrainingPlanExercises.AddRange(trainingPlanExercises);
            _context.SaveChanges();
        }

        public void Delete(TrainingPlanExercise trainingPlanExercise)
        {
            _context.TrainingPlanExercises.Remove(trainingPlanExercise);
            _context.SaveChanges();
        }

        public void Delete(List<TrainingPlanExercise> trainingPlanExercises)
        {
            _context.TrainingPlanExercises.RemoveRange(trainingPlanExercises);
            _context.SaveChanges();
        }*/
    }
}
