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

        // I created these temporart properties to hold the user input from the form. 
        // These will be used to fill the constructor of the player class which is read only so I cannot reach directly into the main constructor.
        [BindProperty]
        public string InputName { get; set; }

        [BindProperty]
        public int InputAttack { get; set; }

        [BindProperty]
        public int InputAge { get; set; }

        [BindProperty]
        public Positions InputPosition { get; set; }

        [BindProperty]
        public int InputDefence { get; set; }

        [BindProperty]
        public double InputMarketValue { get; set; }

        [BindProperty]
        public int InputComposure { get; set; }

        [BindProperty]
        public int InputAggression { get; set; }

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
            // Creating the player using the user input.
            Player addedPlayer = new Player(InputName, InputAge, InputPosition, InputAttack, InputDefence, InputMarketValue, InputComposure, InputAggression);

            try
            {
                // check if it fits the rules I set.
                playerManager.CreateNewPlayer(addedPlayer);
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