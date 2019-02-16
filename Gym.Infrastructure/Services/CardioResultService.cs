using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class CardioResultService : ICardioResultService
    {
        private readonly ICardioResultRepository _resultRepository;
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public CardioResultService(ICardioResultRepository resultRepository, ITrainingDayRepository trainingDayRepository, IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _trainingDayRepository = trainingDayRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public CardioResultDTO Get(Guid id)
        {
            if (_resultRepository.IsExist(id) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<CardioResult, CardioResultDTO>(_resultRepository.Get(id));
        }

        public IEnumerable<CardioResultDTO> Get(TrainingDay trainingDay)
        {
            if (_resultRepository.IsExist(trainingDay) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<IEnumerable<CardioResult>, IEnumerable<CardioResultDTO>>(_resultRepository.Get(trainingDay));
        }

        public IEnumerable<CardioResultDTO> Get(Exercise exercise)
        {
            if (_resultRepository.IsExist(exercise) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<IEnumerable<CardioResult>, IEnumerable<CardioResultDTO>>(_resultRepository.Get(exercise));
        }

        public void CreateNew(TrainingDayDTO trainingDayDTO, ExerciseDTO exerciseDTO, int distance, string time)
        {
            var exercise = _mapper.Map<ExerciseDTO, Exercise>(exerciseDTO);
            var trainingDay = _mapper.Map<TrainingDayDTO, TrainingDay>(trainingDayDTO);

            if (_resultRepository.IsExist(trainingDay, exercise))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newCardioResult = new CardioResult(trainingDay, exercise, distance, time);

            _resultRepository.Add(newCardioResult);
        }

        public void Update(CardioResultDTO cardioResultDTO, int distance, string time)
        {
            var existResult = _mapper.Map<CardioResultDTO, CardioResult>(cardioResultDTO);
            existResult.Update(distance, time);
            _resultRepository.Update(existResult);
        }

        public void Delete(Guid id)
        {
            var existResult = _resultRepository.Get(id);

            if (existResult == null)
                throw new Exception("Can not delte, provided result not exist");

            _resultRepository.Delete(existResult);
        }
    }
}
