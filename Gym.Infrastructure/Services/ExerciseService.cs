using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
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

        public ExerciseDTO Get(string name)
            => _mapper.Map<Exercise, ExerciseDTO>(Single(_exerciseRepository.Get(name)));


        public ExerciseDTO Get(Guid id)
            => _mapper.Map<Exercise, ExerciseDTO>(Single(_exerciseRepository.Get(id)));


        public IEnumerable<ExerciseDTO> Get(Category category)
            => _mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(Collection(_exerciseRepository.Get(category)));


        public IEnumerable<ExerciseDTO> GetAll()
            => _mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(Collection(_exerciseRepository.GetAll()));

        public void CreateNew(string name, Category category)
        {
            if (_exerciseRepository.IsExist(name))
                throw new Exception($"{ErrorsCodes.ItemExist}");

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
                throw new Exception("Can not delete default exercise.");

            _exerciseRepository.Delete(exerciseToDelete);
        }
    }
}
