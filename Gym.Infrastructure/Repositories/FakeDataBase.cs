using Gym.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Repositories
{
    public sealed class FakeDataBase
    {
        private static FakeDataBase _singleton = null;

        public List<Exercise> Exercises { get; set; }
        public List<TrainingPlan> TrainingPlans { get; set; }
        public List<TrainingDay> TrainingDay { get; set; }
        public List<WeightResult> WeightResults { get; set; }
        public List<CardioResult> CardioResults { get; set; }

        public static FakeDataBase GetInstance()
        {
            if (_singleton == null)
                _singleton = new FakeDataBase();

            return _singleton;
        }

        private FakeDataBase()
        {
            Exercises = createExercisesList();
            TrainingPlans = createTrainingPlansList();
            TrainingDay = createTrainingDayList();
            WeightResults = createWeightResultsList();
            CardioResults = createCardioResultsList();
        }

        private List<Exercise> createExercisesList()
        {
            return new List<Exercise>
            {
                new Exercise("Bench press", Category.Chest),
                new Exercise("Squad", Category.Legs),
                new Exercise("Squad2", Category.Legs),
                new Exercise("Planks", Category.Abs),
                new Exercise("OHP", Category.Shoulders),
                new Exercise("Deadth lift", Category.Back),
                new Exercise("Cycling", Category.Cardio)
            };
        }

        private List<TrainingPlan> createTrainingPlansList()
        {
            return new List<TrainingPlan>
            {
                new TrainingPlan("Plan A", Exercises),
                new TrainingPlan("Plan B", Exercises.Take(3).ToList())
            };
        }

        private List<TrainingDay> createTrainingDayList()
        {
            return new List<TrainingDay>
            {
                new TrainingDay(TrainingPlans.First(), "trainig A"),
                new TrainingDay(TrainingPlans.Last(), "trainig B"),
            };
        }

        private List<WeightResult> createWeightResultsList()
        {
            var weightExercise = Exercises.Where(x => x.Category != Category.Cardio);

            return new List<WeightResult>()
            {
                new WeightResult(TrainingDay.First(), weightExercise.First(), 3, 5f, 8),
                new WeightResult(TrainingDay.First(), weightExercise.Last(), 4, 5.5f, 9),
                new WeightResult(TrainingDay.First(), weightExercise.Skip(3).First(), 7, 10.5f, 10),
                new WeightResult(TrainingDay.Last(), weightExercise.First(), 3, 5f, 8),
                new WeightResult(TrainingDay.Last(), weightExercise.Last(), 4, 5.5f, 9),
                new WeightResult(TrainingDay.Last(), weightExercise.Skip(3).First(), 7, 10.5f, 10),
            };
        }

        private List<CardioResult> createCardioResultsList()
        {
            var cardioExercise = Exercises.First(x => x.Category == Category.Cardio);

            return new List<CardioResult>()
            {
                new CardioResult(TrainingDay.First(), cardioExercise, 3, "12:12:12"),
                new CardioResult(TrainingDay.First(), cardioExercise, 4, "13:14:15"),
            };
        }
    }
}
