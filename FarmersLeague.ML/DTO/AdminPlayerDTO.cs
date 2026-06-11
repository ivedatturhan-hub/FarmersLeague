
namespace FarmersLeague.ML.DTOs
{
    public class AdminPlayerDTO
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefence { get; set; }
        public double MarketValue { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsStarting { get; set; }
        public int Condition { get; set; } 
        public int Happiness { get; set; }
        public int Composure { get; set; }
        public int Aggression { get; set; }
        public int SeasonGoals { get; set; }
        public int SeasonAssists { get; set; }
        public int YellowCards { get;  set; } 
        public int RedCards { get; set; } 
        public int TeamID { get; set; }
    }
}
