using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public interface IWeightResultService : IService
    {
        WeightResultDTO Get(Guid id);

        IEnumerable<WeightResultDTO> Get(TrainingDay trainingDay);

        IEnumerable<WeightResultDTO> Get(Exercise exercise);

        void CreateNew(TrainingDayDTO trainingDay, ExerciseDTO exercise, int series, float weight, int reps);

        void Update(WeightResultDTO weightResultDTO, int series, float weight, int reps);

        void Delete(Guid id);
    }
}
