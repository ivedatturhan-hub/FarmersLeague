using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmersLeague.ML;
using System;
using FarmersLeague.ML.Services;

namespace FarmersLeague.UI.Pages.Teams
{
    public class CreateAdminTeamModel : PageModel
    {

        [BindProperty]
        public string InputTeamName { get; set; }
        public string Message { get; set; } = "";

        public RedirectToPageResult OnPost()
        {
            Team newTeam = new Team(1, InputTeamName, 100, 0, Team.TeamTactics.Balanced.ToString(), false);
            TeamManager manager = new TeamManager();

            try
            {
                manager.CreateNewTeam(newTeam);
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
