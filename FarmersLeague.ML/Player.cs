namespace FarmersLeague.ML
{
    public class Player
    {
        public int PlayerID { get; set; }
        public int TeamID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; } 
        public int BaseAttack { get; set; }
        public int BaseDefence { get; set; }
        public double MarketValue { get; set; }
        public bool IsStarting { get; set; }
        public bool IsAvailable { get; set; }
        public int Condition { get; set; }
        public int Happiness { get; set; }
        public int Composure { get; set; }
        public int Aggression { get; set; }
        public int SeasonGoals { get; set; } = 0;   
        public int SeasonAssists { get; set; } = 0;
        public int YellowCards { get; set; } = 0;
        public int RedCards { get; set; } = 0;

    }
}
