using System;
using System.Text.RegularExpressions;

namespace Gym.Core.Models
{
    public class Exercise
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Category Category { get; protected set; }
        public bool IsCustom { get; protected set; }

        protected Exercise() { }

        public Exercise(string name, Category category)
        {
            Id = Guid.NewGuid();
            setName(name);
            setCategory(category);
            IsCustom = true;
        }

        public void Update(string name, Category category)
        {
            if (IsCustom)
                throw new Exception("Can not update default exercise");

            if (Category == Category.Cardio && category != Category.Cardio)
                throw new Exception("Can not update exercise asigned to cardio category.");

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
