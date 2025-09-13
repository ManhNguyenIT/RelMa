using Cortex.Mediator.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RelMa.Application.Behaviors;

namespace RelMa.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCortexMediator(
            configuration: configuration,
            handlerAssemblyMarkerTypes: [typeof(DependencyInjection)],
            options =>
            {
                options.AddDefaultBehaviors();
                options.AddOpenQueryPipelineBehavior(typeof(TracingQueryBehavior<,>));
                options.AddOpenCommandPipelineBehavior(typeof(TracingCommandBehavior<,>));
                options.AddOpenCommandPipelineBehavior(typeof(TransactionCommandBehavior<,>));
                options.AddOpenQueryPipelineBehavior(typeof(ValidationQueryBehavior<,>));
                options.AddOpenCommandPipelineBehavior(typeof(ValidationCommandBehavior<,>));
            }
        );

        return services;
    }
}
