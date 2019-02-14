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

        public void CreateNew(string name, BodyPart bodyPart)
        {
            if (_exerciseRepository.IsExist(name))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newExercise = new Exercise(name, bodyPart);

            _exerciseRepository.Add(newExercise);
        }

        public ExerciseDTO Get(string name)
        {
            return _mapper.Map<Exercise, ExerciseDTO>(_exerciseRepository.Get(name));
        }

        public ExerciseDTO Get(Guid id)
        {
            return _mapper.Map<Exercise, ExerciseDTO>(_exerciseRepository.Get(id));
        }

        public IEnumerable<ExerciseDTO> Get(BodyPart bodyPart)
        {
            return _mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(_exerciseRepository.Get(bodyPart));
        }

        public IEnumerable<ExerciseDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(_exerciseRepository.GetAll());
        }
    }
}
