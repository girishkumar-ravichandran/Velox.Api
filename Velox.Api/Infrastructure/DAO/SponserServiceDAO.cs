using MySqlConnector;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Infrastructure.DAO
{
    public class SponserServiceDAO : ISponserServiceDAO
    {
        private readonly string _connectionString = "server=localhost;database=mydb;user=myuser;password=mypassword";

        public async Task<SponserDTO> RegisterSponser(SponserDTO Sponser)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("AddSponser", connection)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", Sponser.Name);
                    cmd.Parameters.AddWithValue("@StartDate", Sponser.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", Sponser.EndDate);
                    cmd.Parameters.AddWithValue("@Location", Sponser.Location);

                    var outputId = new MySqlParameter("@SponserId", MySqlDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputId);

                    await cmd.ExecuteNonQueryAsync();

                    Sponser.Id = Convert.ToInt32(outputId.Value);

                    return Sponser;
                }
            }
        }


        public async Task<SponserDTO> GetSponserByIdAsync(int SponserId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("GetSponserById", connection)) // Stored procedure for retrieving Sponser by ID
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SponserId", SponserId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new SponserDTO
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

        public async Task<SponserDTO> UpdateSponser(SponserDTO Sponser)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("UpdateSponser", connection)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SponserId", Sponser.Id);
                    cmd.Parameters.AddWithValue("@Name", Sponser.Name);
                    cmd.Parameters.AddWithValue("@StartDate", Sponser.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", Sponser.EndDate);
                    cmd.Parameters.AddWithValue("@Location", Sponser.Location);

                    await cmd.ExecuteNonQueryAsync();

                    return Sponser;
                }
            }
        }


        public async Task DeleteSponser(int SponserId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("DeleteSponser", connection)) // Stored procedure for deleting Sponser
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SponserId", SponserId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<SponserDTO>> GetAllSponsersAsync()
        {
            var Sponsers = new List<SponserDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("GetAllSponsers", connection)) // Stored procedure for retrieving all Sponsers
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Sponsers.Add(new SponserDTO
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

            return Sponsers;
        }


    }
}
