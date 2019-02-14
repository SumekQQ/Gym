using Autofac;
using Gym.Core.Repositories;
using System.Reflection;

namespace Gym.Infrastructure.IoC.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly).Where(x => x.IsAssignableTo<IRepository>()).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
