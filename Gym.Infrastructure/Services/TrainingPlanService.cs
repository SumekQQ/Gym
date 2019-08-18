using AutoMapper;
using Gym.Core.Exceptions;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Services
{
    public class TrainingPlanService : BaseService, ITrainingPlanService
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public TrainingPlanService(ITrainingPlanRepository trainingPlanRepository, IExerciseRepository exerciseRepository, IMapper mapper) : base(mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<TrainingPlanDTO> Get(Guid id)
            => _mapper.Map<TrainingPlan, TrainingPlanDTO>(Single(await _trainingPlanRepository.Get(id)));

        public async Task<IEnumerable<TrainingPlanDTO>> GetAll()
            => _mapper.Map<IEnumerable<TrainingPlan>, IEnumerable<TrainingPlanDTO>>(Collection(await _trainingPlanRepository.GetAll()));

        public async Task CreateNew(string name, IEnumerable<Guid> exercisesId)
        {
            if (await _trainingPlanRepository.IsExist(name))
                throw new ServiceException(ErrorsCodes.ItemExist);

            var exercisesList = Collection(await getExercisesList(exercisesId));
            var newTrainingPlan = new TrainingPlan(name, exercisesList);

            await _trainingPlanRepository.Add(newTrainingPlan);
        }

        public async Task Update(Guid id, string name, IEnumerable<Guid> exercisesId)
        {
            var trainingPlanToUpdate = Single(await _trainingPlanRepository.Get(id));
            var exercisesList = Collection(await getExercisesList(exercisesId)).ToList();

            trainingPlanToUpdate.Update(name, exercisesList);
            await _trainingPlanRepository.Update(trainingPlanToUpdate);
        }

        public async Task Delete(Guid id)
        {
            var trainingPlanToDelete = Single(await _trainingPlanRepository.Get(id));

            await _trainingPlanRepository.Delete(trainingPlanToDelete);
        }

        private async Task<List<Exercise>> getExercisesList(IEnumerable<Guid> exercisesId)
        {
            Collection(exercisesId);
            var exercises = new List<Exercise>();

            foreach (var id in exercisesId)
                exercises.Add(Single(await _exerciseRepository.Get(id)));

            return exercises;
        }
    }
}
