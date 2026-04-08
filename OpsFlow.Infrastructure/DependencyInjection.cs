using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Identity;
using OpsFlow.Infrastructure.Persistence.AppContext;
using OpsFlow.Infrastructure.Persistence.Repositories;
using OpsFlow.Infrastructure.Persistence.UnitOfWork;
using OpsFlow.Infrastructure.Services;

namespace OpsFlow.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
        (
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // DbContext
            // Repositories
            // Identity
            // Services
            // add DbContext to services
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(""));
            });

            // repositories DI
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddScoped<IIncidentHistoryRepository, IncidentHistoryRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            // unitOfWork DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // currentUserService DI
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // services DI
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            // add Identity
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                // user
                options.User.RequireUniqueEmail = true;

                // sign in options
                options.SignIn.RequireConfirmedAccount = true;

                // add password options
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // add lockout options
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}