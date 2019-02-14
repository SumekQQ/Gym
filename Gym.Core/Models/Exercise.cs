using System;
using System.Text.RegularExpressions;

namespace Gym.Core.Models
{
    public class Exercise
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public BodyPart BodyPart { get; protected set; }

        protected Exercise() { }

        public Exercise(string name, BodyPart bodyPart)
        {
            Id = Guid.NewGuid();
            setName(name);
            setBodyPart(bodyPart);
        }

        private void setName(string name)
        {
            var pattern = @"[a-zA-Z0-9._.-]$";
            if (String.IsNullOrEmpty(name) || !Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
                throw new Exception("Provided exercise name is not correct.");

            if (name != Name)
                Name = name;
        }

        private void setBodyPart(BodyPart bodyPart)
        {
            if (BodyPart != bodyPart)
                BodyPart = bodyPart;
        }
    }
}
