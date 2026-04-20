using FarmersLeague.DL; // To see the DTO box
using FarmersLeague.DL.DTO;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
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


    // method for deleting player
    public IActionResult OnPostDelete(int id)
    {
        // 1. Call the manager to delete the player
        PlayerManager manager = new PlayerManager();
        manager.DeletePlayer(id);

        // 2. Refresh the page so the deleted player disappears from the screen!
        return RedirectToPage();
    }
}

