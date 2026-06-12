using FarmersLeague.ML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmersLeague.ML.DTOs;


namespace FarmersLeague.ML.Services
{
    public class TeamManager
    {
        // creating private variables, but using interface, not the real database
        private IPlayerDb playerDb;
        private ITeamDb teamDb;

        // just like the player class, i have a constructor for the team manager for creating a manager. I need to choose which db i use.
        public TeamManager(IPlayerDb PlayerDb, ITeamDb TeamDb)
        {
            playerDb = PlayerDb;
            teamDb = TeamDb;
        }


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


        // method for deleting teams from the db
        public void DeleteTeam(int teamID)
        {
            // add rules here later
            teamDb.DeleteTeam(teamID);
        }

        // method for updating a team
        public void UpdateTheTeam(AdminTeamDTO updatedTeam)
        {
            // add the same rules as in create new team here later
            teamDb.UpdateTeam(updatedTeam);
        }


        // method for getting a team by id
        public AdminTeamDTO GetTeamByID(int teamID)
        {
            return teamDb.GetTeamByID(teamID);
        }


        // method for getting the team that the user controls
        public AdminTeamDTO GetUserControlledTeam()
        {
            AdminTeamDTO team = teamDb.GetUserControlledTeam();

            if (team == null)
            {
                throw new Exception("No user-controlled team found.");
            }
            return team;
        }
    }
}



