using FarmersLeague.DL;
using FarmersLeague.ML;
using FarmersLeague.ML.Interfaces;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace FarmersLeague.UI.Pages.Teams
{
    public class CreateAdminTeamModel : PageModel
    {

        [BindProperty]
        public string InputTeamName { get; set; }
        public string Message { get; set; } = "";


        // creating an empty box for the manager at the top so whole page can use it.
        private TeamManager teamManager;

        public CreateAdminTeamModel()
        {
            //building the spesific databases i want to use
            IPlayerDb playerDb = new PlayerDb();
            ITeamDb teamDb = new TeamDb();

            // putting the databases into the manager constructor.
            teamManager = new TeamManager(playerDb, teamDb);
        }

        public RedirectToPageResult OnPost()
        {
            Team newTeam = new Team(1, InputTeamName, 100, 0, Team.TeamTactics.Balanced.ToString(), false);

            try
            {
                teamManager.CreateNewTeam(newTeam);
                Message = "Team created successfully!";
            }
            catch (Exception ex)
            {
                Message = $"Error: {ex.Message}";
            }
            return RedirectToPage("/Teams/AdminTeams");
        }
    }
}
