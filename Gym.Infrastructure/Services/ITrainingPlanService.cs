using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Services
{
    public interface ITrainingPlanService : IService
    {
        Task<TrainingPlanDTO> Get(Guid id);

        Task<IEnumerable<TrainingPlanDTO>> GetAll();

        Task CreateNew(string name, IEnumerable<Guid> exercisesId);

        Task Update(Guid id, string name, IEnumerable<Guid> exercisesId);

        Task Delete(Guid id);
    }
}
