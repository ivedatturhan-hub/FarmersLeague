using FarmersLeague.DL; // To use AdminTeamDTO
using FarmersLeague.ML;
using FarmersLeague.ML.Services; // To use TeamManager
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FarmersLeague.UI.Pages
{
    public class AdminTeamsModel : PageModel
    {
        // This is the list the HTML will look at to draw the screen
        public List<AdminTeamDTO> TeamList { get; set; }

        public void OnGet()
        {
            //when the page loads, ask the manager for the data\
            TeamManager manager = new TeamManager();
            TeamList = manager.GetAllTeamsForAdmin();
        }

        // method for deleting teams
        public IActionResult OnPostDelete(int id)
        {
            // 1. Call the manager to delete the team
            TeamManager manager = new TeamManager();
            manager.DeleteTeam(id);
            // 2. Refresh the page so the deleted team disappears from the screen!
            return RedirectToPage();
        }
    }
}