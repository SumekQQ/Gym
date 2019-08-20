using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Services
{
    public interface ICardioResultService : IService
    {
        Task<CardioResultDTO> Get(Guid id);

        Task<IEnumerable<CardioResultDTO>> Get(TrainingDay trainingDay);

        Task<IEnumerable<CardioResultDTO>> Get(Exercise exercise);

        Task CreateNew(Guid trainingDayId, Guid exerciseId, int distance, string time);

        Task Update(Guid id, int distance, string time);

        Task Delete(Guid id);
    }
}
