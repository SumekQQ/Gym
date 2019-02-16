using System;

namespace Gym.Core.Models
{
    public abstract class Result
    {
        public Guid Id { get; protected set; }
        public TrainingDay TrainingDay { get; protected set; }
        public Exercise Exercise { get; protected set; }

        protected Result() { }

        protected Result(TrainingDay trainingDay, Exercise exercise)
        {
            Id = Guid.NewGuid();
            TrainingDay = trainingDay;
            SetExercise(exercise);
        }

        protected virtual void SetExercise(Exercise exercise)
        {
            if (Exercise != exercise)
                Exercise = exercise;
        }
    }
}
