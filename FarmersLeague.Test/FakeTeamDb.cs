using FarmersLeague.ML.DTOs;
using FarmersLeague.ML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmersLeague.Test
{
    public class FakeTeamDb : ITeamDb
    {
        private AdminTeamDTO fakeTeam;


        public FakeTeamDb(AdminTeamDTO FakeTeam)
        {
            fakeTeam = FakeTeam;
        }

        public FakeTeamDb()
        {

        }

        public void CreateTeam(int leagueID, string teamName, double budget, int points, string tactics, bool isUserControlled)
        {
            throw new NotImplementedException();
        }

        public void DeleteTeam(int teamID)
        {
            throw new NotImplementedException();
        }

        public List<AdminTeamDTO> GetAllTeamsForAdmin()
        {
            throw new NotImplementedException();
        }

        // The manager will call this, so we hand back a team with only 500 euros!
        public AdminTeamDTO GetTeamByID(int teamID)
        {
            return fakeTeam;
        }

        public AdminTeamDTO GetUserControlledTeam()
        {
            throw new NotImplementedException();
        }

        public void UpdateTeam(AdminTeamDTO updatedTeam)
        {
            throw new NotImplementedException();
        }

        public void UpdateTeamBudget(int teamID, double newBudget)
        {
            fakeTeam.Budget = newBudget;
        }
    }
}