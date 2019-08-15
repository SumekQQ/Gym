using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Core.Exceptions
{
    public class ServiceException : GymException
    {
        public ServiceException()
        {
        }

        public ServiceException(string code) : base(code)
        {
        }

        public ServiceException(string message, params object[] args) : base(message, args)
        {
        }

        public ServiceException(string code, string message, params object[] args) : base(code, message, args)
        {
        }

        public ServiceException(Exception innerException, string message, params object[] args) : base(innerException, message, args)
        {
        }

        public ServiceException(Exception innerException, string code, string message, params object[] args) : base(innerException, code, message, args)
        {
        }
    }
}
