using Gym.Core.Exceptions;
using System;

namespace Gym.Core.Models
{
    public class WeightResult : Result
    {
        public int Series { get; protected set; }
        public float Weight { get; protected set; }
        public int Reps { get; protected set; }

        protected WeightResult() : base() { }

        public WeightResult(TrainingDay trainingDay, Exercise exercise, int series, float weight, int reps) : base(trainingDay, exercise)
        {
            Update(series, weight, reps);
        }

        public void Update(int series, float weight, int reps)
        {
            setSeries(series);
            setWeight(weight);
            setReps(reps);
        }

        protected override void SetExercise(Exercise exercise)
        {
            if (exercise.Category == Category.Cardio)
                throw new DomainException(ErrorsCodes.IncorrectCategory, $"Can not create as cardio result {exercise.Category.ToString()}.");

            base.SetExercise(exercise);
        }

        private void setSeries(int series)
        {
            if (series < 1)
                throw new DomainException(ErrorsCodes.NeagtiveValue, "Provided amount of series less than 0.");

            if (Series != series)
                Series = series;
        }

        private void setWeight(float weight)
        {
            if (weight < 0)
                throw new DomainException(ErrorsCodes.NeagtiveValue, "Provided weights less than 0.");

            if (Weight != weight)
                Weight = weight;
        }

        private void setReps(int reps)
        {
            if (reps < 0)
                throw new DomainException(ErrorsCodes.NeagtiveValue, "Provided amount of reps less than 0.");

            if (Reps != reps)
                Reps = reps;
        }
    }
}
