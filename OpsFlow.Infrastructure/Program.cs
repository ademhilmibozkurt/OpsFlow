using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Infrastructure.Identity;
using OpsFlow.Infrastructure.Persistence.AppContext;
using OpsFlow.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// add DbContext to services
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

// dependency injection
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

var app = builder.Build();
app.UseAuthentication();
app.Run();
