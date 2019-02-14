using Autofac;
using Gym.Infrastructure.Services;
using System.Reflection;

namespace Gym.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly).Where(x => x.IsAssignableTo<IService>()).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
