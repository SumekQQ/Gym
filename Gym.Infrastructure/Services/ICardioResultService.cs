using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public interface ICardioResultService : IService
    {
        CardioResultDTO Get(Guid id);

        IEnumerable<CardioResultDTO> Get(TrainingDay trainingDay);

        IEnumerable<CardioResultDTO> Get(Exercise exercise);

        void CreateNew(TrainingDayDTO trainingDay, ExerciseDTO exercise, int distance, string time);

        void Update(CardioResultDTO cardioResultDTO, int distance, string time);

        void Delete(Guid id);
    }
}
