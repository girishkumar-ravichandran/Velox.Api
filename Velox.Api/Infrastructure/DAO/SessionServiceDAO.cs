using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Middleware.Services;

namespace Velox.Api.Infrastructure.DAO
{
    public class SessionServiceDAO : ISessionServiceDAO
    {
        private readonly string _connectionString;

        public SessionServiceDAO(DatabaseConfigService dbConfigService)
        {
            _connectionString = dbConfigService.GetConnectionString();
        }

        // Create Session
        public async Task<(bool isSuccess, string sessionId, string message)> CreateSessionAsync(int userId, string sessionData)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("sp_CreateSession", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("p_userId", userId);
                    cmd.Parameters.AddWithValue("p_sessionData", sessionData);
                    cmd.Parameters.AddWithValue("p_expiresAt", DateTime.UtcNow.AddHours(1));

                    // Output Parameters
                    var sessionIdParam = new MySqlParameter("op_sessionId", MySqlDbType.VarChar, 36)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(sessionIdParam);

                    var successParam = new MySqlParameter("op_isSuccess", MySqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(successParam);

                    var messageParam = new MySqlParameter("op_message", MySqlDbType.VarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(messageParam);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();

                        // Retrieve output parameters
                        bool isSuccess = Convert.ToBoolean(successParam.Value);
                        string sessionId = sessionIdParam.Value?.ToString();
                        string message = messageParam.Value?.ToString();

                        return (isSuccess, sessionId, message);
                    }
                    catch (MySqlException ex)
                    {
                        return (false, null, $"Database error: {ex.Message}");
                    }
                }
            }
        }

        // Get Session by ID
        public async Task<SessionDTO> GetSessionByIdAsync(string sessionId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("sp_GetSessionById", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameter
                    cmd.Parameters.AddWithValue("p_sessionId", sessionId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new SessionDTO
                            {
                                SessionId = reader["SessionId"].ToString(),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                SessionData = reader["SessionData"].ToString(),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                ExpiresAt = Convert.ToDateTime(reader["ExpiresAt"])
                            };
                        }
                    }
                }
            }
            return null; // Return null if session is not found
        }

        // Delete Session
        public async Task<(bool isSuccess, string message)> DeleteSessionAsync(string sessionId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("sp_DeleteSession", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameter
                    cmd.Parameters.AddWithValue("p_sessionId", sessionId);

                    // Output Parameters
                    var successParam = new MySqlParameter("op_isSuccess", MySqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(successParam);

                    var messageParam = new MySqlParameter("op_message", MySqlDbType.VarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(messageParam);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();

                        bool isSuccess = Convert.ToBoolean(successParam.Value);
                        string message = messageParam.Value?.ToString();

                        return (isSuccess, message);
                    }
                    catch (MySqlException ex)
                    {
                        return (false, $"Database error: {ex.Message}");
                    }
                }
            }
        }

        // Cleanup Expired Sessions
        public async Task<int> CleanupExpiredSessionsAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("sp_CleanupExpiredSessions", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var deletedCountParam = new MySqlParameter("op_deletedCount", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(deletedCountParam);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        return Convert.ToInt32(deletedCountParam.Value);
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"Database error: {ex.Message}");
                        return 0;
                    }
                }
            }
        }
    }
}
