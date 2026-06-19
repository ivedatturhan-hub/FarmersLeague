    namespace FarmersLeague.ML.DTOs
{
    public class AdminTeamDTO
    {
        public int TeamID { get; set; } 
        public int LeagueID { get; set; }
        public string TeamName { get; set; }
        public double Budget { get; set; } 
        public int Points { get; set; }
        public string Tactics { get; set; }
        public bool IsUserControlled { get; set; }
    }
}

