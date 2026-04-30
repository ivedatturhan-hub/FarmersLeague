using FarmersLeague.DL.DTO;
using FarmersLeague.ML;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmersLeague.UI.Pages
{
    public class EditAdminPlayersModel : PageModel
    {
        [BindProperty]
        public AdminPlayerDTO PlayerToEdit { get; set; }

        public void OnGet(int id) 
        {
            PlayerManager manager = new PlayerManager();


            PlayerToEdit = manager.GetPlayerById(id);
        }

        //  runs when user clicks save button.
        public IActionResult OnPost()
        {
            PlayerManager manager = new PlayerManager();

            // executes the update method in the manager.
            manager.UpdatePlayer(PlayerToEdit);

            return RedirectToPage("/Players/AdminPlayers");
        }
    }
}
