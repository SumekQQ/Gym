using Gym.Core.Models;
using System;

namespace Gym.Core.Models
{
    public class TrainingPlanExercise
    {
        public Guid TrainigPlanId { get; protected set; }
        public TrainingPlan TrainigPlan { get; protected set; }
        public Guid ExerciseId { get; protected set; }
        public Exercise Exercise { get; protected set; }

        protected TrainingPlanExercise() { }

        public TrainingPlanExercise(TrainingPlan trainingPlan, Exercise exercise)
        {
            TrainigPlan = trainingPlan;
            TrainigPlanId = trainingPlan.Id;
            Exercise = exercise;
            ExerciseId = exercise.Id;
        }
    }
}
