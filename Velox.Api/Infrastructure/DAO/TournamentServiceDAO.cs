using MySqlConnector;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Infrastructure.DAO
{
    public class TournamentServiceDAO : ITournamentServiceDAO
    {
        private readonly string _connectionString = "server=localhost;database=mydb;user=myuser;password=mypassword";

        public async Task<TournamentDTO> RegisterTournament(TournamentDTO tournament)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("AddTournament", connection)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", tournament.TournamentName);
                    cmd.Parameters.AddWithValue("@StartDate", tournament.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", tournament.EndDate);
                    cmd.Parameters.AddWithValue("@Location", tournament.Location);

                    var outputId = new MySqlParameter("@TournamentId", MySqlDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputId);

                    await cmd.ExecuteNonQueryAsync();

                    tournament.Id = Convert.ToInt32(outputId.Value);

                    return tournament;
                }
            }
        }


        public async Task<TournamentDTO> GetTournamentByIdAsync(int tournamentId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("GetTournamentById", connection)) // Stored procedure for retrieving tournament by ID
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TournamentId", tournamentId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new TournamentDTO
                            {
                                Id = reader.GetInt32("Id"),
                                TournamentName = reader.GetString("Name"),
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

        public async Task<TournamentDTO> UpdateTournament(TournamentDTO tournament)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("UpdateTournament", connection)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TournamentId", tournament.Id);
                    cmd.Parameters.AddWithValue("@Name", tournament.TournamentName);
                    cmd.Parameters.AddWithValue("@StartDate", tournament.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", tournament.EndDate);
                    cmd.Parameters.AddWithValue("@Location", tournament.Location);

                    await cmd.ExecuteNonQueryAsync();

                    return tournament;
                }
            }
        }


        public async Task DeleteTournament(int tournamentId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("DeleteTournament", connection)) // Stored procedure for deleting tournament
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TournamentId", tournamentId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync()
        {
            var tournaments = new List<TournamentDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("GetAllTournaments", connection)) // Stored procedure for retrieving all tournaments
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tournaments.Add(new TournamentDTO
                            {
                                Id = reader.GetInt32("Id"),
                                TournamentName = reader.GetString("Name"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Location = reader.GetString("Location")
                            });
                        }
                    }
                }
            }

            return tournaments;
        }


    }
}
