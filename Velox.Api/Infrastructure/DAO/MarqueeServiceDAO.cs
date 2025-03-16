using MySqlConnector;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Infrastructure.DAO
{
    public class MarqueeServiceDAO : IMarqueeServiceDAO
    {
        private readonly string _connectionString = "server=localhost;database=mydb;user=myuser;password=mypassword";

        public async Task<MarqueeDTO> RegisterMarquee(MarqueeDTO Marquee)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("AddMarquee", connection)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", Marquee.Name);
                    cmd.Parameters.AddWithValue("@StartDate", Marquee.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", Marquee.EndDate);
                    cmd.Parameters.AddWithValue("@Location", Marquee.Location);

                    var outputId = new MySqlParameter("@MarqueeId", MySqlDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputId);

                    await cmd.ExecuteNonQueryAsync();

                    Marquee.Id = Convert.ToInt32(outputId.Value);

                    return Marquee;
                }
            }
        }


        public async Task<MarqueeDTO> GetMarqueeByIdAsync(int MarqueeId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("GetMarqueeById", connection)) // Stored procedure for retrieving Marquee by ID
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MarqueeId", MarqueeId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new MarqueeDTO
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Location = reader.GetString("Location")
                            };
                        }
                        else
                        {
                            return null; 
                        }
                    }
                }
            }
        }

        public async Task<MarqueeDTO> UpdateMarquee(MarqueeDTO Marquee)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("UpdateMarquee", connection)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MarqueeId", Marquee.Id);
                    cmd.Parameters.AddWithValue("@Name", Marquee.Name);
                    cmd.Parameters.AddWithValue("@StartDate", Marquee.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", Marquee.EndDate);
                    cmd.Parameters.AddWithValue("@Location", Marquee.Location);

                    await cmd.ExecuteNonQueryAsync();

                    return Marquee;
                }
            }
        }


        public async Task DeleteMarquee(int MarqueeId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("DeleteMarquee", connection)) // Stored procedure for deleting Marquee
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MarqueeId", MarqueeId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<MarqueeDTO>> GetAllMarqueesAsync()
        {
            var Marquees = new List<MarqueeDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("GetAllMarquees", connection)) // Stored procedure for retrieving all Marquees
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Marquees.Add(new MarqueeDTO
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Location = reader.GetString("Location")
                            });
                        }
                    }
                }
            }

            return Marquees;
        }


    }
}
