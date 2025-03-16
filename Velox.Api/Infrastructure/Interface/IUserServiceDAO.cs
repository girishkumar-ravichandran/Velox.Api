using Velox.Api.Infrastructure.DTO;
namespace Velox.Api.Infrastructure.Interface
{
    public interface IUserServiceDAO
    {
        Task<bool> RegisterUserAsync(UserDTO user);
        Task<bool> ValidateUserLoginAsync(string email, string password);
    }
}
