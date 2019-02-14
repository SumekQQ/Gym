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
        public List<Result> Results { get; set; }

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
            Results = createResultsList();
        }

        private List<Exercise> createExercisesList()
        {
            return new List<Exercise>
            {
                new Exercise("Bench press", BodyPart.Chest),
                new Exercise("Squad", BodyPart.Legs),
                new Exercise("Planks", BodyPart.Stomach),
                new Exercise("OHP", BodyPart.Arms),
                new Exercise("Deadth lift", BodyPart.Back)
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

        private List<Result> createResultsList()
        {
            return new List<Result>
            {
                new Result(TrainingDay.First(), Exercises.First(), 3, 5f, 8),
                new Result(TrainingDay.First(), Exercises.Last(), 4, 5.5f, 9),
                new Result(TrainingDay.First(), Exercises.Skip(3).First(), 7, 10.5f, 10),
                new Result(TrainingDay.Last(), Exercises.First(), 3, 5f, 8),
                new Result(TrainingDay.Last(), Exercises.Last(), 4, 5.5f, 9),
                new Result(TrainingDay.Last(), Exercises.Skip(3).First(), 7, 10.5f, 10),
            };
        }
    }
}
