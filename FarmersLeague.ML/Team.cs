using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmersLeague.ML
{
    public class Team
    {
        public enum TeamTactics
        {
            Balanced,
            Attacking,
            Defensive
        }

        public int TeamID { get; private set; }
        public int LeagueID { get; private set; }
        public string TeamName { get; private set; }
        public decimal Budget { get; private set; } = 100m;
        public int Points { get; private set; } = 0;
        public TeamTactics Tactics { get; private set; } = TeamTactics.Balanced;
        public bool IsUserControlled { get; private set; }
        public List<Player> Players { get; private set; } = new List<Player>();
        


        //constructor for adding team

        public Team(string teamName)
        {
            TeamName = teamName;
        }

        //constructor for database
        public Team(int teamID, int leagueID, string teamName, decimal budget, int points, string tacticsString, bool isUserControlled)
        {
            TeamID = teamID;
            LeagueID = leagueID;
            TeamName = teamName;
            Budget = budget;
            Points = points;
            Tactics = Enum.Parse<TeamTactics>(tacticsString);
            IsUserControlled = isUserControlled;
        }
    }
}
