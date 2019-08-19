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
    public class WeightResultService : BaseService, IWeightResultService
    {
        private readonly IWeightResultRepository _resultRepository;
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public WeightResultService(IWeightResultRepository resultRepository, ITrainingDayRepository trainingDayRepository, IExerciseRepository exerciseRepository, IMapper mapper) : base(mapper)
        {
            _resultRepository = resultRepository;
            _trainingDayRepository = trainingDayRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<WeightResultDTO> Get(Guid id)
            => _mapper.Map<WeightResult, WeightResultDTO>(Single(await _resultRepository.Get(id)));

        public async Task<IEnumerable<WeightResultDTO>> Get(TrainingDay trainingDay)
            => _mapper.Map<IEnumerable<WeightResult>, IEnumerable<WeightResultDTO>>(Collection(await _resultRepository.Get(trainingDay)));

        public async Task<IEnumerable<WeightResultDTO>> Get(Exercise exercise)
            => _mapper.Map<IEnumerable<WeightResult>, IEnumerable<WeightResultDTO>>(Collection(await _resultRepository.Get(exercise)));

        public async Task CreateNew(Guid trainingDayId, Guid exerciseId, int series, float weight, int reps)
        {
            var exercise = Single(await _exerciseRepository.Get(exerciseId));
            var trainingDay = Single(await _trainingDayRepository.Get(trainingDayId));

            if (await _resultRepository.IsExist(trainingDay, exercise))
                throw new ServiceException(ErrorsCodes.ItemExist, $"Cannot create item using trainingDay={trainingDay.Date} and exercise={exercise.Name}. Provided item currently exist.");

            var newWeightResult = new WeightResult(trainingDay, exercise, series, weight, reps);

           await _resultRepository.Add(newWeightResult);
        }

        public async Task Update(Guid id, int series, float weight, int reps)
        {
            var existResult = Single(await _resultRepository.Get(id));

            existResult.Update(series, weight, reps);
            await _resultRepository.Update(existResult);
        }

        public async Task Delete(Guid id)
        {
            var existResult = Single(await _resultRepository.Get(id));

            await _resultRepository.Delete(existResult);
        }
    }
}
