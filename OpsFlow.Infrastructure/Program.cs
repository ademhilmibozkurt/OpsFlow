using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Infrastructure.Identity;
using OpsFlow.Infrastructure.Persistence.AppContext;
using OpsFlow.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// add DbContext to services
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(""));
});

// dependency injection
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

// add Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
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

var app = builder.Build();
app.UseAuthentication();
app.Run();
