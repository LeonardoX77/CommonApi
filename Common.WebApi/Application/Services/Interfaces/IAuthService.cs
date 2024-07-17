using Common.WebApi.Application.Models.User;
using Common.WebApi.Infrastructure.Models.Response;
using Common.Core.Data.Identity;

namespace Common.WebApi.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<BaseResponse> RegisterAsync(UserDto model);
        Task<BaseResponse> LoginAsync(UserDto model);
        string GenerateJwtToken(ApplicationUserBase user);
    }

}
