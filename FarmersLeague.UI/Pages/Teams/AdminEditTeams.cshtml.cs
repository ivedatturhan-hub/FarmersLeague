using FarmersLeague.ML.DTOs;
using FarmersLeague.ML;
using FarmersLeague.ML.Services;
using FarmersLeague.DL;
using FarmersLeague.ML.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FarmersLeague.UI.Pages.Teams
{
    public class AdminEditTeamsModel : PageModel
    {
        // creating an empty box for the manager at the top so whole page can use it.
        private TeamManager teamManager;


        public AdminEditTeamsModel()
        {
            //building the spesific databases i want to use
            IPlayerDb playerDb = new PlayerDb();
            ITeamDb teamDb = new TeamDb();

            // putting the databases into the manager constructor.
            teamManager = new TeamManager(playerDb, teamDb);
        }


        [BindProperty]
        public AdminTeamDTO TeamToEdit { get; set; }

        public void OnGet(int id)
        {
            TeamToEdit = teamManager.GetTeamByID(id);
        }

        //  runs when user clicks save button.
        public IActionResult OnPost()
        {
            // executes the update method in the manager.
            teamManager.UpdateTheTeam(TeamToEdit);
            return RedirectToPage("/Teams/AdminTeams");
        }
    }
}
