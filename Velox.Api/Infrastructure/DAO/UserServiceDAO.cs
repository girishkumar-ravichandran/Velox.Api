using System.Data;
using MySqlConnector;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Middleware.Services;

namespace Velox.Api.Infrastructure.DAO
{
    public class UserServiceDAO : IUserServiceDAO
    {
        private readonly string _connectionString;

        public UserServiceDAO(DatabaseConfigService dbConfigService)
        {
            _connectionString = dbConfigService.GetConnectionString();
        }

        public async Task<bool> RegisterUserAsync(UserDTO user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("sp_insertInitialUserLogin", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("p_email", user.Email);
                    cmd.Parameters.AddWithValue("p_passwordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("p_salt", user.PasswordSalt);
                    cmd.Parameters.AddWithValue("p_gender", user.Gender);
                    cmd.Parameters.AddWithValue("p_dob", user.DOB);
                    cmd.Parameters.AddWithValue("p_phoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("p_role", user.Role);

                    // Output Parameter
                    var successParam = new MySqlParameter("is_success", MySqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(successParam);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        return Convert.ToBoolean(successParam.Value); // Returns true if registration is successful
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception($"Database error: {ex.Message}");
                    }
                }
            }
        }

        public async Task<bool> ValidateUserLoginAsync(string username, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("sp_validateUserLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Input Parameters
                        command.Parameters.AddWithValue("p_username", username);
                        command.Parameters.AddWithValue("p_password", password);

                        // Output Parameter
                        var isValidParam = new MySqlParameter("is_valid", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isValidParam);

                        await command.ExecuteNonQueryAsync();

                        // Retrieve output parameter value
                        return Convert.ToBoolean(isValidParam.Value);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
        }
    }
}
