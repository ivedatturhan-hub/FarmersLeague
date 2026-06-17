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
        public int LeagueID { get; private set; } = 1; // default value for now, will be updated when I implement the league class
        public string TeamName { get; private set; }
        public double Budget { get; private set; } = 100;
        public int Points { get; private set; } = 0;
        public TeamTactics Tactics { get; private set; } = TeamTactics.Balanced;
        public bool IsUserControlled { get; private set; }
        


        //constructor for adding team

        public Team(int leagueID, string teamName, double budget, int points, string tacticsString, bool isUserControlled )
        {
                LeagueID = leagueID;
                TeamName = teamName;
                Budget = budget;
                Points = points;
                Tactics = Enum.Parse<TeamTactics>(tacticsString);
            IsUserControlled = isUserControlled;

        }

        //constructor for database
        public Team(int teamID, int leagueID, string teamName, double budget, int points, string tacticsString, bool isUserControlled)
        {
            TeamID = teamID;
            LeagueID = leagueID;
            TeamName = teamName;
            Budget = budget;
            Points = points;
            Tactics = Enum.Parse<TeamTactics>(tacticsString);
            IsUserControlled = isUserControlled;
        }



        // model methods


        // method for calculating the team's attack power in a match
        public int GetOverallAttack(List<Player> startingPlayers)
        {
            int totalAttack = 0;
            int numberOfPlayers = startingPlayers.Count;

            if (numberOfPlayers < 11) return 0;

            // calculating the avarage attack of the starting players
            foreach (Player player in startingPlayers)
            {
                totalAttack += player.BaseAttack;
            }

            int teamAttack = totalAttack / numberOfPlayers;

            return teamAttack;
        }

        // method for calculating the team's defence power in a match
        public int GetOverallDefence(List<Player> startingPlayers)
        {
            int totalDefence = 0;
            int numberOfPlayers = startingPlayers.Count;

            if (numberOfPlayers < 11) return 0;

            // calculating the avarage attack of the starting players
            foreach (Player player in startingPlayers)
            {
                totalDefence += player.BaseDefence;
            }

            int teamDefence = totalDefence / numberOfPlayers;

            return teamDefence;
        }

    }
}
