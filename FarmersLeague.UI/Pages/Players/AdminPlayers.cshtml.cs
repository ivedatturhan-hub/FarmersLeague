using FarmersLeague.DL; // To see the DTO box
using FarmersLeague.DL.DTO;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

public class AdminPlayersModel : PageModel
{
    // This is the list the HTML will look at to draw the screen
    public List<AdminPlayerDTO> DisplayPlayers { get; set; }

    public void OnGet()
    {
        // When the page loads, ask the manager for the data!
        PlayerManager manager = new PlayerManager();
        DisplayPlayers = manager.GetAllPlayersForAdmin();
    }
}