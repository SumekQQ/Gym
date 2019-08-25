using System.Threading.Tasks;

namespace Gym.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task Handle(T command);
    }
}
