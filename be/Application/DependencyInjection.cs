using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;
using System.Reflection;

namespace Alumni.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Add FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Add MediatR behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.ValidationBehavior<,>));

        return services;
    }
}
