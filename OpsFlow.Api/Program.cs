using System.Security.Cryptography.Xml;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using OpsFlow.Api.Middleware;
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

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OpsFlow API",
        Version = "v1",
        Description = "OpsFlow Incident Management System API"
    });

    // Jwt Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Jwt Authorization header."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Infrastructure Services
builder.Services.AddInfrastructure(builder.Configuration);

// Application Services
builder.Services.AddApplication();

var app = builder.Build();

// middleware
app.UseGlobalExceptionMiddleware();

// Use Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// use auths
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();
