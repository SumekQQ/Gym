using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

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

        public WeightResultDTO Get(Guid id)
            => _mapper.Map<WeightResult, WeightResultDTO>(Single(_resultRepository.Get(id)));

        public IEnumerable<WeightResultDTO> Get(TrainingDay trainingDay)
            => _mapper.Map<IEnumerable<WeightResult>, IEnumerable<WeightResultDTO>>(Collection(_resultRepository.Get(trainingDay)));

        public IEnumerable<WeightResultDTO> Get(Exercise exercise)
            => _mapper.Map<IEnumerable<WeightResult>, IEnumerable<WeightResultDTO>>(Collection(_resultRepository.Get(exercise)));

        public void CreateNew(Guid trainingDayId, Guid exerciseId, int series, float weight, int reps)
        {
            var exercise = Single(_exerciseRepository.Get(exerciseId));
            var trainingDay = Single(_trainingDayRepository.Get(trainingDayId));

            if (_resultRepository.IsExist(trainingDay, exercise))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newWeightResult = new WeightResult(trainingDay, exercise, series, weight, reps);

            _resultRepository.Add(newWeightResult);
        }

        public void Update(Guid id, int series, float weight, int reps)
        {
            var existResult = Single(_resultRepository.Get(id));

            existResult.Update(series, weight, reps);
            _resultRepository.Update(existResult);
        }

        public void Delete(Guid id)
        {
            var existResult = Single(_resultRepository.Get(id));

            _resultRepository.Delete(existResult);
        }
    }
}
