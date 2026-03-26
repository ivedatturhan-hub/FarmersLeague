using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmersLeague.ML;
using FarmersLeague.BL;

namespace FarmersLeague.Pages
{
    public class AddPlayerModel : PageModel
    {
        // 1. Create unlocked temporary variables to catch the web form data
        [BindProperty]
        public string InputName { get; set; }

        [BindProperty]
        public int InputAttack { get; set; }

        [BindProperty]
        public int InputAge { get; set; }

        [BindProperty]
        public string InputPosition { get; set; }

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
            // 2. Safely create the secure Player object using the constructor!
            Player addedPlayer = new Player(InputName, InputAge, InputPosition, InputAttack, InputDefence, InputMarketValue, InputComposure, InputAggression);

            PlayerManager manager = new PlayerManager();

            try
            {
                // 3. Hand the secure player to the Bouncer
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