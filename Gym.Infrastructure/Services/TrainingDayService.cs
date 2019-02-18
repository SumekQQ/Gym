using AutoMapper;
using Gym.Core.Models;
using Gym.Core.Repositories;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Services
{
    public class TrainingDayService : BaseService ,ITrainingDayService
    {
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly ITrainingPlanRepository _trainingPlanRepository;

        public TrainingDayService(ITrainingDayRepository trainingDayRepository, ITrainingPlanRepository trainingPlanRepository, IMapper mapper):base(mapper)
        {
            _trainingDayRepository = trainingDayRepository;
            _trainingPlanRepository = trainingPlanRepository;
        }

        public TrainingDayDTO Get(DateTime date)
            => _mapper.Map<TrainingDay, TrainingDayDTO>(Single(_trainingDayRepository.Get(date)));        

        public TrainingDayDTO Get(Guid id)
            => _mapper.Map<TrainingDay, TrainingDayDTO>(Single(_trainingDayRepository.Get(id)));


        public IEnumerable<TrainingDayDTO> GetCollection(Guid trainingPlanId)
        {
            var trainingPlan = Single(_trainingPlanRepository.Get(trainingPlanId));

            return _mapper.Map<IEnumerable<TrainingDay>, IEnumerable<TrainingDayDTO>>(Collection(_trainingDayRepository.Get(trainingPlan)));
        }

        public void CreateNew(Guid trainingPlanId, string description)
        {
            var trainingPlan = Single(_trainingPlanRepository.Get(trainingPlanId));

            if (_trainingDayRepository.IsExist(trainingPlan) && _trainingDayRepository.IsExist(DateTime.UtcNow))
                throw new Exception($"{ErrorsCodes.ItemExist}");

            var newTrainingDay = new TrainingDay(trainingPlan, description);

            _trainingDayRepository.Add(newTrainingDay);
        }

        public void Update(Guid id, Guid trainingPlanId, string description)
        {
            var trainingDay = Single(_trainingDayRepository.Get(trainingPlanId));
            var trainingPlan = Single(_trainingPlanRepository.Get(trainingPlanId));

            trainingDay.Update(trainingPlan, description);
            _trainingDayRepository.Update(trainingDay);
        }

        public void Delete(Guid id)
        {
            var trainingDay = Single(_trainingDayRepository.Get(id));

            _trainingDayRepository.Delete(trainingDay);
        }
    }
}
