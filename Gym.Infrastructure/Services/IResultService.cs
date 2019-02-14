using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public interface IResultService : IService
    {
        ResultDTO Get(Guid id);

        IEnumerable<ResultDTO> Get(TrainingDay trainingDay);

        IEnumerable<ResultDTO> Get(Exercise exercise);

        void CreateNew(TrainingDayDTO trainingDay, ExerciseDTO exercise, int series, float weight, int reps);
    }
}
