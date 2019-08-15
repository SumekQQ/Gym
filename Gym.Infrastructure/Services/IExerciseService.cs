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
        IEnumerable<ExerciseDTO> GetAll();

        ExerciseDTO Get(Guid id);

        void CreateNew(string name, Category category);

        void Update(Guid id, string name, Category category);

        void Delete(Guid id);
    }
}
