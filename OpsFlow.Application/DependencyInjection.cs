using FluentValidation;
using MediatR;
using OpsFlow.Application.Common.Behaviors;

namespace OpsFlow.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // MediatR Assembly Reference
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

            // FluentValidation Assembly Reference
            services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);

            // ValidationPipeline
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));

            return services;
        }
    }
    
}