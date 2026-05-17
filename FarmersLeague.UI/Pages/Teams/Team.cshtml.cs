using FarmersLeague.DL;
using FarmersLeague.ML;
using FarmersLeague.ML.Interfaces;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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


        // creating an empty box for the manager at the top so whole page can use it.
        TeamManager teamManager;

        public AddTeamModel()
        {
            //building the spesific databases i want to use in this page
            IPlayerDb playerDb = new PlayerDb();
            ITeamDb teamDb = new TeamDb();

            // putting the databases into the manager constructor.
            teamManager = new TeamManager(playerDb, teamDb);
        }

        public void OnPost()
        {
            Team addedTeam = new Team(1, InputTeamName, 100, 0, Team.TeamTactics.Balanced.ToString(), true);

            try
            {
                teamManager.CreateNewTeam(addedTeam);
                Message = "You named your team. Your legend begins!";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }
    }
}
