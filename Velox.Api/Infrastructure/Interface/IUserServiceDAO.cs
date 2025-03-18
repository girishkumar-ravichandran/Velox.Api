using Velox.Api.Infrastructure.DTO;
namespace Velox.Api.Infrastructure.Interface
{
    public interface IUserServiceDAO
    {
        Task<(bool isSuccess, string message)> RegisterUserAsync(UserDTO user);
        Task<(bool isLoginSuccess, bool isUserLocked, bool isUserValidated, bool isPendingRegistration, string message)> ValidateUserLoginAsync(string username, string password);
        Task<(bool isSuccess, string message)> ValidateUserOTPAsync(string username, string otp);
        Task<(string otp, string smtp, bool isSuccess, string message)> GetUserOTPAsync(string username);
    }
}
