using AutoMapper;
using Gym.Core.Exceptions;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Services
{
    public class ExerciseService : BaseService, IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper) : base(mapper)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<ExerciseDTO> Get(Guid id)
            => _mapper.Map<Exercise, ExerciseDTO>(Single(await _exerciseRepository.Get(id)));

        public async Task<IEnumerable<ExerciseDTO>> GetAll()
            => _mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(Collection(await _exerciseRepository.GetAll()));

        public async Task CreateNew(string name, Category category)
        {
            if (await _exerciseRepository.IsExist(name))
                throw new ServiceException(ErrorsCodes.ItemExist, $"Cannot create item using name={name}. Provided item currently exist.");

            var newExercise = new Exercise(name, category);

            await _exerciseRepository.Add(newExercise);
        }

        public async Task Update(Guid id, string name, Category category)
        {
            var exerciseToUpdate = Single(await _exerciseRepository.Get(id));

            exerciseToUpdate.Update(name, category);
            await _exerciseRepository.Update(exerciseToUpdate);
        }

        public async Task Delete(Guid id)
        {
            var exerciseToDelete = Single(await _exerciseRepository.Get(id));

            if (exerciseToDelete.IsDefault)
                throw new DomainException(ErrorsCodes.DefaultExercise, "Can not delete default exercise.");

            await _exerciseRepository.Delete(exerciseToDelete);
        }
    }
}
