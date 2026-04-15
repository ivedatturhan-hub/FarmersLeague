using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmersLeague.ML;
using System;


namespace FarmersLeague.UI.Pages.Teams
{
    public class AddTeamModel : PageModel
    {
        // I created these temporary properties to hold the user input from the form. 
        // These will be used to fill the constructor of the player class which is read only so I cannot reach directly into the main constructor

        [BindProperty]
        public string InputTeamName { get; set; }
        public string Message { get; set; } = "";

        public void OnPost()  //learn what this does
        {
            Team addedTeam = new Team(1, InputTeamName, 100, 0, Team.TeamTactics.Balanced.ToString(), true);
            TeamManager manager = new TeamManager();

            try
            {
                manager.CreateNewTeam(addedTeam);
                Message = "You named your team. Your legend begins!";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }
    }
}
