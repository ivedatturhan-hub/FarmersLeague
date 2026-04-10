namespace FarmersLeague.ML
{
    public class Player
    {
        public int PlayerID { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Position { get; private set; }
        public int BaseAttack { get; private set; }
        public int BaseDefence { get; private set; }
        public double MarketValue { get; private set; }
        public bool IsAvailable { get; private set; }
        public int Condition { get; private set; } = 100;
        public int Happiness { get; private set; } = 100;
        public int Composure { get; private set; }
        public int Aggression { get; private set; }
        public int SeasonGoals { get; private set; } = 0;   
        public int SeasonAssists { get; private set; } = 0;
        public int YellowCards { get; private set; } = 0;
        public int RedCards { get; private set; } = 0;

         // constructor for adding a new player 
        public Player(string name, int age, string position, int baseAttack, int baseDefence, double marketValue, int composure, int aggression)
        {
            Name = name;
            Age = age;
            Position = position;
            BaseAttack = baseAttack;
            BaseDefence = baseDefence;
            MarketValue = marketValue;
            Composure = composure;
            Aggression = aggression;
        }

        // constructor for loading a player from the database (includes every property)
        public Player(int playerID, string name, int age, string position, int baseAttack, int baseDefence, double marketValue,
        bool isAvailable, int condition, int happiness, int composure, int aggression, int seasonGoals, int seasonAssists, int yellowCards, int redCards)
        {
            PlayerID = playerID;
            Name = name;
            Age = age;
            Position = position;
            BaseAttack = baseAttack;
            BaseDefence = baseDefence;
            MarketValue = marketValue;
            IsAvailable = isAvailable;
            Condition = condition;
            Happiness = happiness;
            Composure = composure;
            Aggression = aggression;
            SeasonGoals = seasonGoals;
            SeasonAssists = seasonAssists;
            YellowCards = yellowCards;
            RedCards = redCards;
        }

    }
}
