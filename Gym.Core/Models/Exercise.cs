using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Gym.Core.Models
{
    public class Exercise
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Category Category { get; protected set; }
        public bool IsDefault { get; protected set; }
        protected Exercise() { }

        public Exercise(string name, Category category)
        {
            Id = Guid.NewGuid();
            setName(name);
            setCategory(category);
            IsDefault = false;
        }

        public void Update(string name, Category category)
        {
            if (IsDefault)
                throw new Exception("Can not update default exercise");

            if (Category == Category.Cardio && category != Category.Cardio)
                throw new Exception("Can not update category exercise asigned to cardio category.");

            if (Category != Category.Cardio && category == Category.Cardio)
                throw new Exception("Can not update to cardio category exercise asigned to weight category.");

            setName(name);
            setCategory(Category);
        }

        private void setName(string name)
        {
            var pattern = @"[a-zA-Z0-9._.-]$";
            if (String.IsNullOrEmpty(name) || !Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
                throw new Exception("Provided exercise name is not correct.");

            if (name != Name)
                Name = name;
        }

        private void setCategory(Category category)
        {
            if (Category != category)
                Category = category;
        }
    }
}
