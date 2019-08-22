using System.Threading.Tasks;

namespace Gym.Infrastructure.Commands
{
    public interface ICommandDispatcher
    {
        Task Dispatch<T>(T command) where T : ICommand;
    }
}
