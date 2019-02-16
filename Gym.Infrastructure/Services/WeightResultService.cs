using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class WeightResultService : IWeightResultService
    {
        private readonly IWeightResultRepository _resultRepository;
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public WeightResultService(IWeightResultRepository resultRepository, ITrainingDayRepository trainingDayRepository, IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _trainingDayRepository = trainingDayRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public WeightResultDTO Get(Guid id)
        {
            if (_resultRepository.IsExist(id) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<WeightResult, WeightResultDTO>(_resultRepository.Get(id));
        }

        public IEnumerable<WeightResultDTO> Get(TrainingDay trainingDay)
        {
            if (_resultRepository.IsExist(trainingDay) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<IEnumerable<WeightResult>, IEnumerable<WeightResultDTO>>(_resultRepository.Get(trainingDay));
        }

        public IEnumerable<WeightResultDTO> Get(Exercise exercise)
        {
            if (_resultRepository.IsExist(exercise) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<IEnumerable<WeightResult>, IEnumerable<WeightResultDTO>>(_resultRepository.Get(exercise));
        }

        public void CreateNew(TrainingDayDTO trainingDayDTO, ExerciseDTO exerciseDTO, int series, float weight, int reps)
        {
            var exercise = _mapper.Map<ExerciseDTO, Exercise>(exerciseDTO);
            var trainingDay = _mapper.Map<TrainingDayDTO, TrainingDay>(trainingDayDTO);

            if (_resultRepository.IsExist(trainingDay, exercise))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newWeightResult = new WeightResult(trainingDay, exercise, series, weight, reps);

            _resultRepository.Add(newWeightResult);
        }

        public void Update(WeightResultDTO existResultDTO, int series, float weight, int reps)
        {
            var existResult = _mapper.Map<WeightResultDTO, WeightResult>(existResultDTO);
            existResult.Update(series, weight, reps);
            _resultRepository.Update(existResult);
        }

        public void Delete(Guid id)
        {
            var existResult = _resultRepository.Get(id);

            if (existResult == null)
                throw new Exception("Can not delete, provided result not exist");

            _resultRepository.Delete(existResult);
        }
    }
}
