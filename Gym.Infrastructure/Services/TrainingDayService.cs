using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class TrainingDayService : ITrainingDayService
    {
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IMapper _mapper;

        public TrainingDayService(ITrainingDayRepository trainingDayRepository, ITrainingPlanRepository trainingPlanRepository, IMapper mapper)
        {
            _trainingDayRepository = trainingDayRepository;
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
        }

        public void CreateNew(TrainingPlanDTO trainingPlanDTO, string description)
        {
            var trainingPlan = _mapper.Map<TrainingPlanDTO, TrainingPlan>(trainingPlanDTO);

            if (_trainingDayRepository.IsExist(trainingPlan) && _trainingDayRepository.IsExist(DateTime.UtcNow))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newTrainingDay = new TrainingDay(trainingPlan, description);

            _trainingDayRepository.Add(newTrainingDay);
        }

        public TrainingDayDTO Get(DateTime date)
        {
            if (_trainingDayRepository.IsExist(date) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<TrainingDay, TrainingDayDTO>(_trainingDayRepository.Get(date));
        }

        public TrainingDayDTO Get(Guid id)
        {
            if (_trainingDayRepository.IsExist(id) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<TrainingDay, TrainingDayDTO>(_trainingDayRepository.Get(id));
        }

        public IEnumerable<TrainingDayDTO> Get(TrainingPlan trainingPlan)
        {
            if (_trainingPlanRepository.IsExist(trainingPlan) == false)
                throw new Exception("Provided training plan not exist");

            if (_trainingDayRepository.IsExist(trainingPlan) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<IEnumerable<TrainingDay>, IEnumerable<TrainingDayDTO>>(_trainingDayRepository.Get(trainingPlan));
        }
    }
}
