using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Services
{
    public interface ITrainingDayService : IService
    {
        Task<TrainingDayDTO> Get(Guid id);

        Task<IEnumerable<TrainingDayDTO>> GetAll();

        Task CreateNew(Guid trainingPlanId, string description, string date);

        Task Update(Guid id, Guid trainingPlanId, string description);

        Task Delete(Guid id);
    }
}
