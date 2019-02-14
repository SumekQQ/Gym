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
        private readonly IMapper _mapper;

        public TrainingPlanService(ITrainingPlanRepository trainingPlanRepository, IMapper mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
        }

        public void CreateNew(string name, IEnumerable<ExerciseDTO> exercises)
        {
            if (_trainingPlanRepository.IsExist(name))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newTrainingPlan = new TrainingPlan(name, _mapper.Map<IEnumerable<ExerciseDTO>, IEnumerable<Exercise>>(exercises));

            _trainingPlanRepository.Add(newTrainingPlan);
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
    }
}
