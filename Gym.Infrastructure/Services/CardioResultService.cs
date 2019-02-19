using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class CardioResultService : BaseService, ICardioResultService
    {
        private readonly ICardioResultRepository _resultRepository;
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public CardioResultService(ICardioResultRepository resultRepository, ITrainingDayRepository trainingDayRepository, IExerciseRepository exerciseRepository, IMapper mapper) : base(mapper)
        {
            _resultRepository = resultRepository;
            _trainingDayRepository = trainingDayRepository;
            _exerciseRepository = exerciseRepository;
        }

        public CardioResultDTO Get(Guid id)
            => _mapper.Map<CardioResult, CardioResultDTO>(Single(_resultRepository.Get(id)));

        public IEnumerable<CardioResultDTO> Get(TrainingDay trainingDay)
            => _mapper.Map<IEnumerable<CardioResult>, IEnumerable<CardioResultDTO>>(Collection(_resultRepository.Get(trainingDay)));

        public IEnumerable<CardioResultDTO> Get(Exercise exercise)
            => _mapper.Map<IEnumerable<CardioResult>, IEnumerable<CardioResultDTO>>(Collection(_resultRepository.Get(exercise)));

        public void CreateNew(Guid trainingDayId, Guid exerciseId, int distance, string time)
        {
            var exercise = Single(_exerciseRepository.Get(exerciseId));
            var trainingDay = Single(_trainingDayRepository.Get(trainingDayId));

            if (_resultRepository.IsExist(trainingDay, exercise))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newCardioResult = new CardioResult(trainingDay, exercise, distance, time);

            _resultRepository.Add(newCardioResult);
        }

        public void Update(Guid id, int distance, string time)
        {
            var existResult = Single(_resultRepository.Get(id));

            existResult.Update(distance, time);
            _resultRepository.Update(existResult);
        }

        public void Delete(Guid id)
        {
            var existResult = Single(_resultRepository.Get(id));

            _resultRepository.Delete(existResult);
        }
    }
}
