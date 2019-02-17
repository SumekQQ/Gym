using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class TrainingPlanService : BaseService, ITrainingPlanService
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public TrainingPlanService(ITrainingPlanRepository trainingPlanRepository, IExerciseRepository exerciseRepository, IMapper mapper) : base(mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _exerciseRepository = exerciseRepository;
        }

        public TrainingPlanDTO Get(Guid id)
            => _mapper.Map<TrainingPlan, TrainingPlanDTO>(Single(_trainingPlanRepository.Get(id)));

        public TrainingPlanDTO Get(string name)
            => _mapper.Map<TrainingPlan, TrainingPlanDTO>(Single(_trainingPlanRepository.Get(name)));

        public IEnumerable<TrainingPlanDTO> GetAll()
            => _mapper.Map<IEnumerable<TrainingPlan>, IEnumerable<TrainingPlanDTO>>(Collection(_trainingPlanRepository.GetAll()));

        public void CreateNew(string name, IEnumerable<Guid> exercisesId)
        {
            if (_trainingPlanRepository.IsExist(name))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var exercisesList = Collection(getExercisesList(exercisesId));
            var newTrainingPlan = new TrainingPlan(name, exercisesList);

            _trainingPlanRepository.Add(newTrainingPlan);
        }

        public void Update(Guid id, string name, IEnumerable<Guid> exercisesId)
        {
            var trainingPlanToUpdate = Single(_trainingPlanRepository.Get(id));
            var exercisesList = Collection(getExercisesList(exercisesId));

            trainingPlanToUpdate.Update(name, exercisesList);
            _trainingPlanRepository.Update(trainingPlanToUpdate);
        }

        public void Delete(Guid id)
        {
            var trainingPlanToDelete = Single(_trainingPlanRepository.Get(id));

            _trainingPlanRepository.Delete(trainingPlanToDelete);
        }

        private List<Exercise> getExercisesList(IEnumerable<Guid> exercisesId)
        {
            Collection(exercisesId);
            var exercises = new List<Exercise>();

            foreach (var id in exercisesId)
                exercises.Add(Single(_exerciseRepository.Get(id)));

            return exercises;
        }
    }
}
