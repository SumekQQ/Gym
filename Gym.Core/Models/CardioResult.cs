﻿using Gym.Core.Exceptions;
using System;

namespace Gym.Core.Models
{
    public class CardioResult : Result
    {
        public int Distance { get; protected set; }
        public string Time { get; set; }

        protected CardioResult() : base() { }

        public CardioResult(TrainingDay trainingDay, Exercise exercise, int distance, string time) : base(trainingDay, exercise)
        {
            setDistance(distance);
            setTimeCardio(time);
        }

        public void Update(int distance, string time)
        {
            setDistance(distance);
            setTimeCardio(time);
        }

        protected override void SetExercise(Exercise exercise)
        {
            if (exercise.Category != Category.Cardio)
                throw new DomainException(ErrorsCodes.IncorrectCategory, $"Can not create {exercise.Category.ToString()} as cardio result");

            base.SetExercise(exercise);
        }

        private void setDistance(int distance)
        {
            if (distance <= 0)
                throw new DomainException(ErrorsCodes.NeagtiveValue, "Exercise distance can not be less than 0.");

            if (distance != Distance)
                Distance = distance;
        }

        private void setTimeCardio(string time)
        {
            var singleTime = time.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            var singleTimeAsInt = new int[singleTime.Length];
            time = "";

            if (singleTime.Length < 3)
                throw new DomainException(ErrorsCodes.IncorrectTimeFormats, "Training time should contain only hours, mins and seconds");


            for (int i = 0; i < singleTime.Length; i++)
            {
                if (Int32.TryParse(singleTime[i], out singleTimeAsInt[i]) == false)
                    throw new DomainException(ErrorsCodes.IncorrectTimeFormats, $"Can not convert {singleTime[i]} to time value");

                if (i != 0 && singleTimeAsInt[i] >= 60)
                {
                    singleTimeAsInt[i - 1] += singleTimeAsInt[i] / 60;
                    singleTimeAsInt[i] %= 60;
                }

                time += (singleTimeAsInt[i].ToString() + ":");
            }

            if (Time != time)
                Time = time;
        }
    }
}
