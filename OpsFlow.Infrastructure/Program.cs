using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// dependency injection
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

var app = builder.Build();
app.Run();
