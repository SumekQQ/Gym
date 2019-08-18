using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Services
{
    public interface IExerciseService : IService
    {
        Task<IEnumerable<ExerciseDTO>> GetAll();

        Task<ExerciseDTO> Get(Guid id);

        Task CreateNew(string name, Category category);

        Task Update(Guid id, string name, Category category);

        Task Delete(Guid id);
    }
}
