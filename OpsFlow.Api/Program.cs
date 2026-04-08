using System.Text;
using Microsoft.IdentityModel.Tokens;
using OpsFlow.Application;
using OpsFlow.Infrastructure;
using OpsFlow.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// add jwt settings configuration
var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>()
    ?? throw new NullReferenceException("Jwt settings is null!");

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

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Infrastructure Services
builder.Services.AddInfrastructure(builder.Configuration);

// Application Services
builder.Services.AddApplication();

var app = builder.Build();

// use auths
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();
