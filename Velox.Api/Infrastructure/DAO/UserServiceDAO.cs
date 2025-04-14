using System.Data;
using MySqlConnector;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.DTO.ResponseDTO;
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

        public async Task<RegisterResponseDTO> RegisterUserAsync(UserDTO user)
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
                    cmd.Parameters.AddWithValue("p_firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("p_lastName", user.LastName);

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

                        bool isSuccess = Convert.ToBoolean(successParam.Value);
                        string message = messageParam.Value?.ToString();

                        return new RegisterResponseDTO
                        {
                            IsRegistrationSuccess = Convert.ToBoolean(successParam.Value),
                        };
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception($"Database error: {ex.Message}");
                    }
                }
            }
        }


        public async Task<LoginResponseDTO> ValidateUserLoginAsync(string username, string password)
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

                        return new LoginResponseDTO
                        {
                            Email = username,
                            IsLoginSuccess = Convert.ToBoolean(isLoginSuccessParam.Value),
                            IsUserLocked = Convert.ToBoolean(isUserLockedParam.Value),
                            IsUserValidated = Convert.ToBoolean(isUserValidatedParam.Value),
                            IsPendingRegistration = Convert.ToBoolean(isPendingRegistrationParam.Value),
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }


        public async Task<ValidateUserOTPResponseDTO> ValidateUserOTPAsync(string username, string otp)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("sp_ValidateUserOTP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_username", username);
                        command.Parameters.AddWithValue("p_otp", otp);

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

                        return new ValidateUserOTPResponseDTO
                        {
                            IsSuccess = Convert.ToBoolean(isSuccessParam.Value),
                            Message = messageParam.Value?.ToString()
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }

        public async Task<GetUserOTPResponseDTO> GetUserOTPAsync(string username)
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

                        return new GetUserOTPResponseDTO
                        {
                            OTP = otpParam.Value?.ToString(),
                            SMTP = smtpParam.Value?.ToString(),
                            IsSuccess = Convert.ToBoolean(isSuccessParam.Value),
                            Message = messageParam.Value?.ToString()
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }

        public async Task<ForgetPasswordResponseDTO> ForgetPasswordAsync(string Email, string newPasswordHash)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("sp_resetUserPassword", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_email", Email);
                    cmd.Parameters.AddWithValue("p_newPasswordHash", newPasswordHash);

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

                        bool isSuccess = Convert.ToBoolean(successParam.Value);
                        string message = messageParam.Value?.ToString();

                        return new ForgetPasswordResponseDTO
                        {
                            IsSuccess = Convert.ToBoolean(successParam.Value),
                            Message = messageParam.Value?.ToString()
                        };
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception($"Database error: {ex.Message}");
                    }
                }
            }
        }


    }
}
