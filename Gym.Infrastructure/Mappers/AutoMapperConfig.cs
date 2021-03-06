﻿using AutoMapper;
using Gym.Core.Models;
using Gym.Infrastructure.DTO;
using System;
using System.Globalization;
using System.Linq;

namespace Gym.Infrastructure.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Exercise, ExerciseDTO>();

                cfg.CreateMap<Result, WeightResultDTO>();

                cfg.CreateMap<TrainingDay, TrainingDayDTO>()
                    .ForMember(dest => dest.TrainingPlan,
                        opt => opt.MapFrom(src =>
                            src.TrainingPlan.Id))
                    .ForMember(dest => dest.Date,
                         opt => opt.MapFrom(src => 
                            src.Date.ToString("d.M.yyyy")));

                cfg.CreateMap<TrainingPlan, TrainingPlanDTO>()
                    .ForMember(dest => dest.ExerciseIds, 
                        opt => opt.MapFrom(src=> 
                            src.ExerciseIds.Select(x=>x.ExerciseId).ToList()));
            }).CreateMapper();
        }
    }
}
