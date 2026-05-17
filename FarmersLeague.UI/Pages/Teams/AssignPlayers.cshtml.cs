using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmersLeague.ML.Services;
using FarmersLeague.ML;
using FarmersLeague.ML.DTOs;
using FarmersLeague.ML.Interfaces;
using FarmersLeague.DL;

public class AssignPlayersModel : PageModel
{
    // creating an empty box for the manager at the top so whole page can use it.
    private PlayerManager playerManager;

    // The Constructor 
    public AssignPlayersModel()
    {
        // building the databases i want to use in this page
        IPlayerDb myPlayerDb = new PlayerDb();
        ITeamDb myTeamDb = new TeamDb();  // if i want to use another db for any reason i need to create a new db and change the db name after the "new".

        // put them into the manager constructor
        playerManager = new PlayerManager(myPlayerDb, myTeamDb);
    }
   


    // creating the list of players
    public List<AdminPlayerDTO> PlayersWithNoTeam { get; set; } = new List<AdminPlayerDTO>();

    // bind property catches the value of selected player and team from the form 
    [BindProperty]
    public int SelectedPlayerID { get; set; }

    [BindProperty]
    public int TargetTeamID { get; set; }

    public void OnGet(int teamId)
    {
        TargetTeamID = teamId;
        PlayersWithNoTeam = playerManager.GetPlayersWithNoTeam();
    }

    public IActionResult OnPost()
    {
        playerManager.UpdatePlayerTeam(SelectedPlayerID, TargetTeamID);
        return RedirectToPage("/Teams/TeamPlayers", new { id = TargetTeamID });
    }
}