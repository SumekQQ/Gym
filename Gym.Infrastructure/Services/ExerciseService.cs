using AutoMapper;
using Gym.Core.Exceptions;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class ExerciseService : BaseService, IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper) : base(mapper)
        {
            _exerciseRepository = exerciseRepository;
        }

        public IEnumerable<ExerciseDTO> GetAll()
            => _mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(Collection(_exerciseRepository.GetAll()));

        public ExerciseDTO Get(Guid id)
            => _mapper.Map<Exercise, ExerciseDTO>(_exerciseRepository.Get(id));


        public void CreateNew(string name, Category category)
        {
            if (_exerciseRepository.IsExist(name))
                throw new ServiceException(ErrorsCodes.ItemExist, $"Cannot create item using name={name}. Provided item currently exist.");

            var newExercise = new Exercise(name, category);

            _exerciseRepository.Add(newExercise);
        }

        public void Update(Guid id, string name, Category category)
        {
            var exerciseToUpdate = Single(_exerciseRepository.Get(id));

            exerciseToUpdate.Update(name, category);
            _exerciseRepository.Update(exerciseToUpdate);
        }

        public void Delete(Guid id)
        {
            var exerciseToDelete = Single(_exerciseRepository.Get(id));

            if (exerciseToDelete.IsDefault)
                throw new DomainException(ErrorsCodes.DefaultExercise, "Can not delete default exercise.");

            _exerciseRepository.Delete(exerciseToDelete);
        }
    }
}
