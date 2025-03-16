using MySqlConnector;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Infrastructure.DAO
{
    public class PanelistServiceDAO : IPanelistServiceDAO
    {
        private readonly string _connectionString = "server=localhost;database=mydb;user=myuser;password=mypassword";

        public async Task<PanelistDTO> RegisterPanelist(PanelistDTO Panelist)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("AddPanelist", connection)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", Panelist.Name);
                    cmd.Parameters.AddWithValue("@StartDate", Panelist.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", Panelist.EndDate);
                    cmd.Parameters.AddWithValue("@Location", Panelist.Location);

                    var outputId = new MySqlParameter("@PanelistId", MySqlDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputId);

                    await cmd.ExecuteNonQueryAsync();

                    Panelist.Id = Convert.ToInt32(outputId.Value);

                    return Panelist;
                }
            }
        }


        public async Task<PanelistDTO> GetPanelistByIdAsync(int PanelistId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("GetPanelistById", connection)) // Stored procedure for retrieving Panelist by ID
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PanelistId", PanelistId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new PanelistDTO
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

        public async Task<PanelistDTO> UpdatePanelist(PanelistDTO Panelist)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("UpdatePanelist", connection)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PanelistId", Panelist.Id);
                    cmd.Parameters.AddWithValue("@Name", Panelist.Name);
                    cmd.Parameters.AddWithValue("@StartDate", Panelist.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", Panelist.EndDate);
                    cmd.Parameters.AddWithValue("@Location", Panelist.Location);

                    await cmd.ExecuteNonQueryAsync();

                    return Panelist;
                }
            }
        }


        public async Task DeletePanelist(int PanelistId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("DeletePanelist", connection)) // Stored procedure for deleting Panelist
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PanelistId", PanelistId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<PanelistDTO>> GetAllPanelistsAsync()
        {
            var Panelists = new List<PanelistDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("GetAllPanelists", connection)) // Stored procedure for retrieving all Panelists
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Panelists.Add(new PanelistDTO
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

            return Panelists;
        }


    }
}
