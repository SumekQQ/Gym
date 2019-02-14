using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public ResultService(IResultRepository resultRepository, ITrainingDayRepository trainingDayRepository, IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _trainingDayRepository = trainingDayRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public void CreateNew(TrainingDayDTO trainingDayDTO, ExerciseDTO exerciseDTO, int series, float weight, int reps)
        {
            var exercise = _mapper.Map<ExerciseDTO, Exercise>(exerciseDTO);
            var trainingDay = _mapper.Map<TrainingDayDTO, TrainingDay>(trainingDayDTO);

            if (_resultRepository.IsExist(trainingDay, exercise))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newResult = new Result(trainingDay, exercise, series, weight, reps);

            _resultRepository.Add(newResult);
        }

        public ResultDTO Get(Guid id)
        {
            if (_resultRepository.IsExist(id) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<Result, ResultDTO>(_resultRepository.Get(id));
        }

        public IEnumerable<ResultDTO> Get(TrainingDay trainingDay)
        {
            if (_resultRepository.IsExist(trainingDay) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<IEnumerable<Result>, IEnumerable<ResultDTO>>(_resultRepository.Get(trainingDay));
        }

        public IEnumerable<ResultDTO> Get(Exercise exercise)
        {
            if (_resultRepository.IsExist(exercise) == false)
                throw new Exception($"{ErrorsCodes.ItemNotFound}");

            return _mapper.Map<IEnumerable<Result>, IEnumerable<ResultDTO>>(_resultRepository.Get(exercise));
        }
    }
}
