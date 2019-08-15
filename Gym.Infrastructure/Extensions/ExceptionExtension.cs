using Gym.Core.Exceptions;

namespace Gym.Infrastructure.Extensions
{
    public static class ExceptionExtension
    {
        public static object PrintException(GymException ex)
        {
            return new
            {
                ClassName = ex.GetType(),
                ErrorCode = ex.Code,
                Message = ex.Message,
                Source = ex.Source
            };
        }
    }
}
