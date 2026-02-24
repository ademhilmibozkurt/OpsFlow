using OpsFlow.Application.Identity;
using OpsFlow.Application.Models;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface ITokenService
    {
        TokenResultModel GenerateTokens(AppUser user);
    }
}