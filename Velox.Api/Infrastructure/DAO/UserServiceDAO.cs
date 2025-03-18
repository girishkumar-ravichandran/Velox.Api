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

        public async Task<(bool isSuccess, string message)> RegisterUserAsync(UserDTO user)
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

                    // Output Parameters
                    var successParam = new MySqlParameter("op_issuccess", MySqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(successParam);

                    var messageParam = new MySqlParameter("op_messagetext", MySqlDbType.VarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(messageParam);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();

                        // Retrieve output parameters
                        bool isSuccess = Convert.ToBoolean(successParam.Value);
                        string message = messageParam.Value?.ToString();

                        return (isSuccess, message); // Return the result along with the message
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception($"Database error: {ex.Message}");
                    }
                }
            }
        }


        public async Task<(bool isLoginSuccess, bool isUserLocked, bool isUserValidated, bool isPendingRegistration, string message)> ValidateUserLoginAsync(string username, string password)
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

                        // Output Parameters
                        var isLoginSuccessParam = new MySqlParameter("op_isloginsuccess", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isLoginSuccessParam);

                        var isUserLockedParam = new MySqlParameter("op_isuserlocked", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isUserLockedParam);

                        var isUserValidatedParam = new MySqlParameter("op_isuservalidated", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isUserValidatedParam);

                        var isPendingRegistrationParam = new MySqlParameter("op_ispendingregistration", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isPendingRegistrationParam);

                        var messageParam = new MySqlParameter("op_messagetext", MySqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(messageParam);

                        await command.ExecuteNonQueryAsync();

                        // Retrieve output parameter values
                        bool isLoginSuccess = Convert.ToBoolean(isLoginSuccessParam.Value);
                        bool isUserLocked = Convert.ToBoolean(isUserLockedParam.Value);
                        bool isUserValidated = Convert.ToBoolean(isUserValidatedParam.Value);
                        bool isPendingRegistration = Convert.ToBoolean(isPendingRegistrationParam.Value);
                        string message = messageParam.Value?.ToString();

                        return (isLoginSuccess, isUserLocked, isUserValidated, isPendingRegistration, message);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return (false, false, false, false, $"Error: {ex.Message}");
            }
        }


        public async Task<(bool isSuccess, string message)> ValidateUserOTPAsync(string username, string otp)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("sp_ValidateUserOTP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Input Parameters
                        command.Parameters.AddWithValue("p_username", username);
                        command.Parameters.AddWithValue("p_otp", otp);

                        // Output Parameters
                        var isSuccessParam = new MySqlParameter("op_issuccess", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isSuccessParam);

                        var messageParam = new MySqlParameter("op_messagetext", MySqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(messageParam);

                        // Execute the command
                        await command.ExecuteNonQueryAsync();

                        // Retrieve the output parameters
                        bool isSuccess = Convert.ToBoolean(isSuccessParam.Value);
                        string message = messageParam.Value.ToString();

                        return (isSuccess, message);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return (false, "Error: Database operation failed");
            }
        }

        public async Task<(string otp, string smtp, bool isSuccess, string message)> GetUserOTPAsync(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("sp_getUserOTP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_username", username);

                        var otpParam = new MySqlParameter("op_otp", MySqlDbType.VarChar, 6)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(otpParam);

                        var smtpParam = new MySqlParameter("op_smtp", MySqlDbType.VarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(smtpParam);

                        var isSuccessParam = new MySqlParameter("op_issuccess", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isSuccessParam);

                        var messageParam = new MySqlParameter("op_messagetext", MySqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(messageParam);

                        await command.ExecuteNonQueryAsync();

                        string otp = otpParam.Value.ToString();
                        string smtp = smtpParam.Value.ToString();
                        bool isSuccess = Convert.ToBoolean(isSuccessParam.Value);
                        string message = messageParam.Value.ToString();

                        return (otp, smtp, isSuccess, message);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return (null, null, false, "Database error: Unable to retrieve OTP.");
            }
        }

    }
}
