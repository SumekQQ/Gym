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
                new Exercise("Planks", Category.Abs),
                new Exercise("OHP", Category.Shoulders),
                new Exercise("Deadth lift", Category.Back)
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
            return new List<WeightResult>();
            /*  {
                  new WeightResult(TrainingDay.First(), Exercises.First(), 3, 5f, 8),
                    new WeightResult(TrainingDay.First(), Exercises.Last(), 4, 5.5f, 9),
                    new WeightResult(TrainingDay.First(), Exercises.Skip(3).First(), 7, 10.5f, 10),
                    new WeightResult(TrainingDay.Last(), Exercises.First(), 3, 5f, 8),
                    new WeightResult(TrainingDay.Last(), Exercises.Last(), 4, 5.5f, 9),
                    new WeightResult(TrainingDay.Last(), Exercises.Skip(3).First(), 7, 10.5f, 10),
              };*/
        }

        private List<CardioResult> createCardioResultsList()
        {
            return new List<CardioResult>();
        }
    }
}
