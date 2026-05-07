using FarmersLeague.DL.DTO;
using FarmersLeague.ML;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FarmersLeague.UI.Pages.Teams
{
    public class AdminEditTeamsModel : PageModel
    {

        [BindProperty]
        public AdminTeamDTO TeamToEdit { get; set; }

        public void OnGet(int id)
        {
            TeamManager manager = new TeamManager();

            TeamToEdit = manager.GetTeamByID(id);
        }

        //  runs when user clicks save button.
        public IActionResult OnPost()
        {
            TeamManager manager = new TeamManager();
            // executes the update method in the manager.
            manager.UpdateTheTeam(TeamToEdit);
            return RedirectToPage("/Teams/AdminTeams");
        }
    }
}
