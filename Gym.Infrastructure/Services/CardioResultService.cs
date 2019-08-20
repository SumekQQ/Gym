using AutoMapper;
using Gym.Core.Exceptions;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<CardioResultDTO> Get(Guid id)
            => _mapper.Map<CardioResult, CardioResultDTO>(Single(await _resultRepository.Get(id)));

        public async Task<IEnumerable<CardioResultDTO>> Get(TrainingDay trainingDay)
            => _mapper.Map<IEnumerable<CardioResult>, IEnumerable<CardioResultDTO>>(Collection(await _resultRepository.Get(trainingDay)));

        public async Task<IEnumerable<CardioResultDTO>> Get(Exercise exercise)
            => _mapper.Map<IEnumerable<CardioResult>, IEnumerable<CardioResultDTO>>(Collection(await _resultRepository.Get(exercise)));

        public async Task CreateNew(Guid trainingDayId, Guid exerciseId, int distance, string time)
        {
            var exercise = Single(await _exerciseRepository.Get(exerciseId));
            var trainingDay = Single(await _trainingDayRepository.Get(trainingDayId));

            if (await _resultRepository.IsExist(trainingDay, exercise))
                throw new ServiceException(ErrorsCodes.ItemExist, $"Cannot create item using trainingDay={trainingDay.Date} and exercise={exercise.Name}. Provided item currently exist.");

            var newCardioResult = new CardioResult(trainingDay, exercise, distance, time);

            await _resultRepository.Add(newCardioResult);
        }

        public async Task Update(Guid id, int distance, string time)
        {
            var existResult = Single(await _resultRepository.Get(id));

            existResult.Update(distance, time);
            await _resultRepository.Update(existResult);
        }

        public async Task Delete(Guid id)
        {
            var existResult = Single(await _resultRepository.Get(id));

            await _resultRepository.Delete(existResult);
        }
    }
}
