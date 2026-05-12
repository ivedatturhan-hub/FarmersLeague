using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmersLeague.ML.Services;
using FarmersLeague.DL.DTO;
using FarmersLeague.ML;

public class AssignPlayersModel : PageModel
{

    PlayerManager playerManager = new PlayerManager();

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