using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Services
{
    public interface IWeightResultService : IService
    {
        Task<WeightResultDTO> Get(Guid id);

        Task<IEnumerable<WeightResultDTO>> Get(TrainingDay trainingDay);

        Task<IEnumerable<WeightResultDTO>> Get(Exercise exercise);

        Task CreateNew(Guid trainingDayId, Guid exerciseId, int series, float weight, int reps);

        Task Update(Guid id, int series, float weight, int reps);

        Task Delete(Guid id);
    }
}
