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
                throw new Exception($"Can not create as cardio result {exercise.Category.ToString()}.");

            base.SetExercise(exercise);
        }

        private void setSeries(int series)
        {
            if (series < 1 || series > 20)
                throw new Exception("Provided amount of series is not correct.");

            if (Series != series)
                Series = series;
        }

        private void setWeight(float weight)
        {
            if (weight < 0 || weight > 250)
                throw new Exception("Provided weights is not correct.");

            if (Weight != weight)
                Weight = weight;
        }

        private void setReps(int reps)
        {
            if (reps < 1 || reps > 30)
                throw new Exception("Provided amount of reps is not correct.");

            if (Reps != reps)
                Reps = reps;
        }
    }
}
