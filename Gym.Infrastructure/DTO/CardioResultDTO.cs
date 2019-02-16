using Gym.Core.Models;
using System;

namespace Gym.Infrastructure.DTO
{
    public class CardioResultDTO
    {
        public Guid Id { get; set; }
        public TrainingDay TrainingDay { get; set; }
        public Exercise Exercise { get; set; }
        public int Distance { get; set; }
        public string Time { get; set; }
    }
}
