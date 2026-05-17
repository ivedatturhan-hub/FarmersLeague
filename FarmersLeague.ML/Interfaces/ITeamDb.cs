using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmersLeague.ML.DTOs;
namespace FarmersLeague.ML.Interfaces
{
    public interface ITeamDb
    {
        void CreateTeam(int leagueID, string teamName, double budget, int points, string tactics, bool isUserControlled);
        List<AdminTeamDTO> GetAllTeamsForAdmin();
        void DeleteTeam(int teamID);
        void UpdateTeam(AdminTeamDTO updatedTeam);
        AdminTeamDTO GetTeamByID(int teamID);
        void UpdateTeamBudget(int teamID, double newBudget);
    }
}
