using BuildingBlocks.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OrderingApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehaviours<,>));
                cfg.AddOpenBehavior(typeof(LogginBehaviour<,>));
            });

            return services;
        }
    }
}
