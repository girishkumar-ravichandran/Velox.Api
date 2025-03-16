namespace Velox.Api.Infrastructure.DTO
{
    public class TournamentDTO
    {
        public int Id { get; set; }
        public string TournamentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
