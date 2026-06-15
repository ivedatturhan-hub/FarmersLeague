using FarmersLeague.ML.DTOs;
using FarmersLeague.ML;
using FarmersLeague.ML.Services;
using FarmersLeague.DL;
using FarmersLeague.ML.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FarmersLeague.ML.Player;

namespace FarmersLeague.UI.Pages.Players
{
    public class AddPlayerModel : PageModel
    {
        //creating an empty box for the manager at the top so whole page can use it.
        PlayerManager playerManager;


        // instead of creating a bind property for each input, I can just create one big bind property for the whole form,

        [BindProperty]
        public AdminPlayerDTO newPlayer { get; set; } // One single box to catch the whole form!

        public string Message { get; set; } = "";


        public AddPlayerModel()
        {
            //building the spesific databases i want to use
            IPlayerDb playerDb = new PlayerDb();
            ITeamDb teamDb = new TeamDb();

            // putting the databases into the manager constructor.
            playerManager = new PlayerManager(playerDb, teamDb);
        }



        public IActionResult OnPost()
        {
            try
            {
                // check if it fits the rules I set.
                playerManager.CreateNewPlayer(newPlayer);
                Message = "Success! Player added to the database.";
                return RedirectToPage("/Players/AdminPlayers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            
        }

    }
}