using FarmersLeague.ML.DTOs;
using FarmersLeague.ML;
using FarmersLeague.ML.Services;
using FarmersLeague.DL;
using FarmersLeague.ML.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmersLeague.UI.Pages
{
    public class AdminTeamsModel : PageModel
    {
        // creating an empty box for the manager at the top so whole page can use it.
        TeamManager teamManager;

        // This is the list the HTML will look at to draw the screen
        public List<AdminTeamDTO> TeamList { get; set; }

        public AdminTeamsModel()
        {
            //building the spesific databases i want to use in this page
            IPlayerDb playerDb = new PlayerDb();
            ITeamDb teamDb = new TeamDb();

            // putting the databases into the manager constructor.
            teamManager = new TeamManager(playerDb, teamDb);
        }

        public void OnGet()
        {
            //when the page loads, ask the manager for the data\
            TeamList = teamManager.GetAllTeamsForAdmin();
        }

        // method for deleting teams
        public IActionResult OnPostDelete(int id)
        {
            // 1. Call the manager to delete the team
            teamManager.DeleteTeam(id);
            // 2. Refresh the page so the deleted team disappears from the screen!
            return RedirectToPage();
        }
    }
}