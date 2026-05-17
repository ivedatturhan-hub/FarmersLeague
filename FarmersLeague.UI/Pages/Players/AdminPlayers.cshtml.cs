using FarmersLeague.ML.DTOs;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using FarmersLeague.DL;
using FarmersLeague.ML.Interfaces;

public class AdminPlayersModel : PageModel
{
    // creating an empty box for the manager at the top so whole page can use it.
    private PlayerManager playerManager;


    // This is the list the HTML will look at to draw the screen
    public List<AdminPlayerDTO> PlayerList { get; set; }


    public AdminPlayersModel()
    {
        IPlayerDb playerDb = new PlayerDb();
        ITeamDb teamDb = new TeamDb();

        playerManager = new PlayerManager(playerDb, teamDb);
    }

    public void OnGet()
    {
        PlayerList = playerManager.GetAllPlayersForAdmin();
    }


    // method for deleting player
    public IActionResult OnPostDelete(int id)
    {
        playerManager.DeletePlayer(id);

        // 2. Refresh the page so the deleted player disappears from the screen!
        return RedirectToPage();
    }
}

