using FarmersLeague.ML.DTOs;
using FarmersLeague.ML;
using FarmersLeague.ML.Services;
using FarmersLeague.DL;
using FarmersLeague.ML.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmersLeague.UI.Pages
{
    public class TeamPlayersModel : PageModel
    {
        // creating the list that holds the squad
        public List<AdminPlayerDTO> Squad { get; set; } = new List<AdminPlayerDTO>();

        // keeping track of the team id so we know which team we're looking at in the HTML page
        public int CurrentTeamId { get; set; }

        //creating an empty box for the manager at the top so whole page can use it.
        PlayerManager playerManager;

        public TeamPlayersModel()
        {
            //building the spesific databases i want to use
            IPlayerDb playerDb = new PlayerDb();
            ITeamDb teamDb = new TeamDb();

            // putting the databases into the manager constructor.
            playerManager = new PlayerManager(playerDb, teamDb);
        }




        //OnGet(int id): in the adminteam page, our button looked like this: asp-route-id="@team.TeamID".
        //That means the URL becomes something like .../TeamPlayers?id=5. By putting (int id) inside the OnGet method,
        //ASP.NET automatically grabs that 5 from the URL and hands it to your C# code.
        //(ai explaining) in case i forget the logic

        public void OnGet(int id)
        {
            CurrentTeamId = id;

            // executing the method that gets the players by the team id and assigning it to the squad list
            Squad = playerManager.GetPlayersByTeamID(id);
        }


        public IActionResult OnPostDelete(int id)
        {
            // 1. Call the manager to delete the player
            playerManager.RemovePlayerFromTeam(id);
            return RedirectToPage("/Teams/TeamPlayers", new { id = CurrentTeamId });
        }
    }
}