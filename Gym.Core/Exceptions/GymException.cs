using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Core.Exceptions
{
    public abstract class GymException : Exception
    {
        public string Code { get; }

        protected GymException()
        {
        }

        protected GymException(string code)
        {
            Code = code;
        }

        protected GymException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected GymException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected GymException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected GymException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}

