using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gym.Core.Models
{
    public class TrainingPlan
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public IEnumerable<Exercise> Exercises { get; protected set; }

        protected TrainingPlan() { }

        public TrainingPlan(string name, IEnumerable<Exercise> exercises)
        {
            Id = Guid.NewGuid();
            setName(name);
            setExerciseList(exercises);
        }

        public void Update(string name, IEnumerable<Exercise> exercises)
        {
            setName(name);
            setExerciseList(exercises);
        }

        private void setName(string name)
        {
            var pattern = @"[a-zA-Z0-9._.-]$";
            if (String.IsNullOrEmpty(name) || !Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
                throw new Exception("Provided name is not correct.");

            if (name != Name)
                Name = name;
        }
        
        private void setExerciseList(IEnumerable<Exercise> exercises)
        {
            if (exercises == null || exercises.Count() < 1)
                throw new Exception("Provided exercises list cannot be empty");

            exercises = exercises.OrderBy(x => x.Category);

            if (Exercises != exercises)
                Exercises = exercises;
        }
    }
}
