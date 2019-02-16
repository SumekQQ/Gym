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
        ExerciseDTO Get(string name);

        ExerciseDTO Get(Guid id);

        IEnumerable<ExerciseDTO> Get(Category category);

        IEnumerable<ExerciseDTO> GetAll();

        void CreateNew(string name, Category category);

        void Update(Guid id, string name, Category category);

        void Delete(Guid id);
    }
}
