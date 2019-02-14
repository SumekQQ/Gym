using Gym.Core.Models;
using System;

namespace Gym.Infrastructure.DTO
{
    public class ExerciseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BodyPart BodyPart { get; set; }
    }
}
