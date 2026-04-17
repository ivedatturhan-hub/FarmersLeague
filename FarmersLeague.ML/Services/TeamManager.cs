using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmersLeague.DL;

namespace FarmersLeague.ML.Services
{
    public class TeamManager
    {
        private TeamDb teamDb = new TeamDb();


        // method with rulesfor creating a new team.
        public void CreateNewTeam(Team newTeam)
        {
            // my rules for creating a new team
            if (string.IsNullOrEmpty(newTeam.TeamName))
            {
                throw new Exception("Team name cannot be empty.");
            }

            teamDb.CreateTeam(newTeam.LeagueID, newTeam.TeamName, newTeam.Budget, newTeam.Points, newTeam.Tactics.ToString(), newTeam.IsUserControlled);
        }


        // method to get all teams in a league for the admin page

        public List<AdminTeamDTO> GetAllTeamsForAdmin()
        {
            return teamDb.GetAllTeamsForAdmin();

        }

    }
}



