using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.DTO.ResponseDTO;
namespace Velox.Api.Infrastructure.Interface
{
    public interface IUserServiceDAO
    {
        Task<RegisterResponseDTO> RegisterUserAsync(UserDTO user);
        Task<LoginResponseDTO> ValidateUserLoginAsync(string username, string password);
        Task<ValidateUserOTPResponseDTO> ValidateUserOTPAsync(string username, string otp);
        Task<GetUserOTPResponseDTO> GetUserOTPAsync(string username);
        Task<ForgetPasswordResponseDTO> ForgetPasswordAsync(string userId, string newPasswordHash);
    }
}
