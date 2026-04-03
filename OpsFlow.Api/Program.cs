using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using OpsFlow.Application;
using OpsFlow.Application.Common.Behaviors;
using OpsFlow.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// add jwt settings configuration
var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

// add jwt bearer
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
    });


builder.Services.AddControllers();

// MediatR Assembly Reference
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

// FluentValidation Assembly Reference
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);

// ValidationPipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));

var app = builder.Build();

// use auths
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();
