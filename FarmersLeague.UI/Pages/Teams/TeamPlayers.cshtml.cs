using FarmersLeague.DL;
using FarmersLeague.DL.DTO;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FarmersLeague.UI.Pages
{
    public class TeamPlayersModel : PageModel
    {
        // creating the list that holds the squad
        public List<AdminPlayerDTO> Squad { get; set; } = new List<AdminPlayerDTO>();

        // keeping track of the team id so we know which team we're looking at in the HTML page
        public int CurrentTeamId { get; set; }




        //OnGet(int id): in the adminteam page, our button looked like this: asp-route-id="@team.TeamID".
        //That means the URL becomes something like .../TeamPlayers?id=5. By putting (int id) inside the OnGet method,
        //ASP.NET automatically grabs that 5 from the URL and hands it to your C# code.
        //(ai explaining) in case i forget the logic

        public void OnGet(int id)
        {
            CurrentTeamId = id;

            // executing the method that gets the players by the team id and assigning it to the squad list
            PlayerManager manager = new PlayerManager();
            Squad = manager.GetPlayersByTeamID(id);
        }
    }
}