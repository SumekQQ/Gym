using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public ExerciseDTO Get(string name)
        {
            return _mapper.Map<Exercise, ExerciseDTO>(_exerciseRepository.Get(name));
        }

        public ExerciseDTO Get(Guid id)
        {
            return _mapper.Map<Exercise, ExerciseDTO>(_exerciseRepository.Get(id));
        }

        public IEnumerable<ExerciseDTO> Get(Category category)
        {
            return _mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(_exerciseRepository.Get(category));
        }

        public IEnumerable<ExerciseDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(_exerciseRepository.GetAll());
        }

        public void CreateNew(string name, Category category)
        {
            if (_exerciseRepository.IsExist(name))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newExercise = new Exercise(name, category);

            _exerciseRepository.Add(newExercise);
        }

        public void Update(Guid id, string name, Category category)
        {
            var exerciseToUpdate = _exerciseRepository.Get(id);

            if (exerciseToUpdate == null)
                throw new Exception($"Can not update exercise : {name}, provided exercise not exist.");

            exerciseToUpdate.Update(name, category);
            _exerciseRepository.Update(exerciseToUpdate);
        }

        public void Delete(Guid id)
        {
            var exerciseToDelete = _exerciseRepository.Get(id);

            if (exerciseToDelete == null)
                throw new Exception("Can not delete exercise, provided exercise not exist.");

            if (exerciseToDelete.IsCustom)
                throw new Exception("Can not delete default exercise.");

            _exerciseRepository.Delete(exerciseToDelete);
        }
    }
}
