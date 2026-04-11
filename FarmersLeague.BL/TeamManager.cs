using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmersLeague.ML;
using FarmersLeague.DL;

namespace FarmersLeague.BL
{
    public class TeamManager
    {
        private TeamDb teamDb = new TeamDb();

        public void CreateNewTeam(Team newTeam)
        {
            // my rules for creating a new team
            if (string.IsNullOrEmpty(newTeam.TeamName))
            {
                throw new Exception("Team name cannot be empty.");
            }

            teamDb.CreateTeam(newTeam);
        }
    }
}
