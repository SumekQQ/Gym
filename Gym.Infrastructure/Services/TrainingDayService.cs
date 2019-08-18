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
    public class TrainingDayService : BaseService, ITrainingDayService
    {
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly ITrainingPlanRepository _trainingPlanRepository;

        public TrainingDayService(ITrainingDayRepository trainingDayRepository, ITrainingPlanRepository trainingPlanRepository, IMapper mapper) : base(mapper)
        {
            _trainingDayRepository = trainingDayRepository;
            _trainingPlanRepository = trainingPlanRepository;
        }

        public async Task<TrainingDayDTO> Get(Guid id)
            => _mapper.Map<TrainingDay, TrainingDayDTO>(Single(await _trainingDayRepository.Get(id)));

        public async Task<IEnumerable<TrainingDayDTO>> GetAll()
            => _mapper.Map<IEnumerable<TrainingDay>, IEnumerable<TrainingDayDTO>>(Collection(await _trainingDayRepository.GetAll()));

        public async Task CreateNew(Guid trainingPlanId, string description, string date)
        {
            var trainingPlan = Single(await _trainingPlanRepository.Get(trainingPlanId));
            var dateTime = DateTime.Parse(date);

            if (await _trainingDayRepository.IsExist(trainingPlan) && await _trainingDayRepository.IsExist(dateTime))
                throw new ServiceException(ErrorsCodes.ItemExist, $"Cannot create item using date={dateTime}. Provided item currently exist.");

            var newTrainingDay = new TrainingDay(trainingPlan, description, dateTime);

            await _trainingDayRepository.Add(newTrainingDay);
        }

        public async Task Update(Guid id, Guid trainingPlanId, string description)
        {
            var trainingDay = Single(await _trainingDayRepository.Get(trainingPlanId));
            var trainingPlan = Single(await _trainingPlanRepository.Get(trainingPlanId));

            trainingDay.Update(trainingPlan, description);
            await _trainingDayRepository.Update(trainingDay);
        }

        public async Task Delete(Guid id)
        {
            var trainingDay = Single(await _trainingDayRepository.Get(id));

            await _trainingDayRepository.Delete(trainingDay);
        }

    }
}
