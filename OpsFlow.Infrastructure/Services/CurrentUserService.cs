using System.Security.Claims;
using OpsFlow.Application.Abstractions.Services;

namespace OpsFlow.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    
        // getUser - from http
        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        // getUserId
        public string? UserId => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // getUserName
        public string? UserName => User?.FindFirst(ClaimTypes.Name)?.Value;

        // getEmail
        public string? Email => User?.FindFirst(ClaimTypes.Email)?.Value;
        
        // getRole
        public string? Role => User?.FindFirst(ClaimTypes.Role)?.Value;
        
        // getIsAuth
        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false; 
    }
}