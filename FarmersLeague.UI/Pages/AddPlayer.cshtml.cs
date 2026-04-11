using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmersLeague.ML;
using FarmersLeague.BL;
using static FarmersLeague.ML.Player;

namespace FarmersLeague.Pages
{
    public class AddPlayerModel : PageModel
    {
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

        public void OnPost()
        {
            // Creating the player using the user input.
            Player addedPlayer = new Player(InputName, InputAge, InputPosition, InputAttack, InputDefence, InputMarketValue, InputComposure, InputAggression);

            PlayerManager manager = new PlayerManager();

            try
            {
                // check if it fits the rules I set.
                manager.CreateNewPlayer(addedPlayer);
                Message = "Success! Player added to the database.";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}