using System;

namespace Gym.Core.Models
{
    public class TrainingDay
    {
        public Guid Id { get; protected set; }
        public DateTime Date { get; protected set; }
        public TrainingPlan TrainingPlan { get; set; }
        public string Description { get; set; }

        protected TrainingDay() { }

        public TrainingDay(TrainingPlan trainingPlan, string description)
        {
            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            setTrainingPlan(trainingPlan);
            Description = description;
        }

        public void Update(TrainingPlan trainingPlan, string description)
        {
            setTrainingPlan(trainingPlan);
            Description = description;
        }

        private void setTrainingPlan(TrainingPlan trainingPlan)
        {
            if (trainingPlan == null)
                throw new Exception("Provided training plan cannot be empty");

            if (TrainingPlan != trainingPlan)
                TrainingPlan = trainingPlan;
        }
    }
}
