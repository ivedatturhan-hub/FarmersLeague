using FarmersLeague.ML.DTOs;
using FarmersLeague.ML;
using FarmersLeague.ML.Services;
using FarmersLeague.DL;
using FarmersLeague.ML.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmersLeague.UI.Pages
{
    public class EditAdminPlayersModel : PageModel
    {
        // creating an empty box for the manager at the top so whole page can use it.
        private PlayerManager playerManager;

        public EditAdminPlayersModel()
        {
            //building the spesific databases i want to use
            IPlayerDb playerDb = new PlayerDb();
            ITeamDb teamDb = new TeamDb();

            // putting the databases into the manager constructor.
            playerManager = new PlayerManager(playerDb, teamDb);
        }



        [BindProperty]
        public AdminPlayerDTO PlayerToEdit { get; set; }

        public void OnGet(int id) 
        {
            PlayerToEdit = playerManager.GetPlayerById(id);
        }

        //  runs when user clicks save button.
        public IActionResult OnPost()
        {
            // executes the update method in the manager.
            playerManager.UpdateThePlayer(PlayerToEdit);

            return RedirectToPage("/Players/AdminPlayers");
        }
    }
}
