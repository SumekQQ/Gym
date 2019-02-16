using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Services
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public TrainingPlanService(ITrainingPlanRepository trainingPlanRepository, IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public TrainingPlanDTO Get(Guid id)
        {
            if (_trainingPlanRepository.IsExist(id) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<TrainingPlan, TrainingPlanDTO>(_trainingPlanRepository.Get(id));
        }

        public TrainingPlanDTO Get(string name)
        {
            if (_trainingPlanRepository.IsExist(name) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<TrainingPlan, TrainingPlanDTO>(_trainingPlanRepository.Get(name));
        }

        public IEnumerable<TrainingPlanDTO> GetAll()
        {
            var trainingPlans = _trainingPlanRepository.GetAll();

            if (trainingPlans.Count() == 0)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<IEnumerable<TrainingPlan>, IEnumerable<TrainingPlanDTO>>(trainingPlans);
        }

        public void CreateNew(string name, IEnumerable<Guid> exercisesId)
        {
            if (_trainingPlanRepository.IsExist(name))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var exercises = new List<Exercise>();
            foreach (var id in exercisesId)
                exercises.Add(_exerciseRepository.Get(id));


            var newTrainingPlan = new TrainingPlan(name, exercises);

            _trainingPlanRepository.Add(newTrainingPlan);
        }

        public void Update(Guid id, string name, IEnumerable<Guid> exercisesId)
        {
            var trainingPlanToUpdate = _trainingPlanRepository.Get(id);

            if (trainingPlanToUpdate == null)
                throw new Exception($"Can not update training plan: {name}, provided plan not exist.");

            var exercises = new List<Exercise>();
            foreach (var exercise in exercisesId)
                exercises.Add(_exerciseRepository.Get(exercise));

            trainingPlanToUpdate.Update(name, exercises);
            _trainingPlanRepository.Update(trainingPlanToUpdate);
        }

        public void Delete(Guid id)
        {
            var trainingPlanToDelete = _trainingPlanRepository.Get(id);

            if (trainingPlanToDelete == null)
                throw new Exception($"Can not delete training plan, provided plan not exist.");

            _trainingPlanRepository.Delete(trainingPlanToDelete);
        }
    }
}
