using Gym.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.EF
{
    public class GymContext : DbContext
    {
        public GymContext(DbContextOptions<GymContext> options) : base(options) { }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<TrainingDay> TrainingDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainingPlanExercise>()
                .HasKey(e => new { e.ExerciseId, e.TrainigPlanId });
        }
    }
}
