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

        void CreateNew(Guid trainingDayId, Guid exerciseId, int series, float weight, int reps);

        void Update(Guid id, int series, float weight, int reps);

        void Delete(Guid id);
    }
}
