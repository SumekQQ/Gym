using System;

namespace Gym.Core.Models
{
    public class Result
    {
        public Guid Id { get; protected set; }
        public TrainingDay TrainingDay { get; protected set; }
        public Exercise Exercise { get; protected set; }
        public int Series { get; protected set; }
        public float Weight { get; protected set; }
        public int Reps { get; protected set; }

        protected Result() { }

        public Result(TrainingDay trainingDay, Exercise exercise, int series, float weight, int reps)
        {
            Id = Guid.NewGuid();
            TrainingDay = trainingDay;
            Exercise = exercise;
            setSeries(series);
            setWeight(weight);
            setReps(reps);
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
