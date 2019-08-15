using AutoMapper;
using Gym.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Infrastructure.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper _mapper;

        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected T Single<T>(T data)
        {
            if (data == null)
                throw new ServiceException(ErrorsCodes.ItemNotFound, $"Finding data not exist or return null value");

            return data;
        }

        protected IEnumerable<T> Collection<T>(IEnumerable<T> data)
        {
            if(data == null || data.Count() < 1)
                throw new ServiceException(ErrorsCodes.ItemNotFound, $"Finding data not exist or return null value");

            return data;
        }
    }
}
