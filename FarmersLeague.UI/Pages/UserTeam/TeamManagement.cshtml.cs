using FarmersLeague.DL;
using FarmersLeague.ML.DTOs;
using FarmersLeague.ML.Interfaces;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;


namespace FarmersLeague.UI.Pages.UserTeam
{
    public class TeamManagementModel: PageModel
    {
        private PlayerManager playerManager;
        private TeamManager teamManager;

        [BindProperty]
        public string SelectedTactic { get; set; }

        [BindProperty]
        public List<int> StarterIDs { get; set; }

        // This is a special box that holds the list of players for our dropdown menus. 
        public SelectList PlayerOptions { get; set; }

        public TeamManagementModel()
        {
            //building the spesific databases i want to use
            IPlayerDb playerDb = new PlayerDb();
            ITeamDb teamDb = new TeamDb();

            playerManager = new PlayerManager(playerDb, teamDb);
            teamManager = new TeamManager(playerDb, teamDb);
        }

        public void OnGet()
        {
            // getting the only team that the user has, and its id, and also the tactics
            var userTeam = teamManager.GetUserControlledTeam();
            SelectedTactic = userTeam.Tactics;
            // getting all the players who plays for the spesific team
            var players = playerManager.GetPlayersByTeamID(userTeam.TeamID);

            // putting these players to a dropdown list I created
            PlayerOptions = new SelectList(players, "PlayerID", "Name");

            // getting the ids of the players who are starting, and putting them in the StarterIDs box. i will use this in html file to show the current starters
            StarterIDs = players.Where(p => p.IsStarting).Select(p => p.PlayerID).ToList();

            // if the team is brand new or missing players, fill the rest of the list with 0s so the HTML loop doesn't crash looking for 11 slots.
            while (StarterIDs.Count < 11)
            {
                StarterIDs.Add(0); // 0 represents "-- Select Player --"
            }

        }


        //saving the changes
        public IActionResult OnPost()
        {
            // --- CHECK 1: Did they pick exactly 11 players? ---
            // .Distinct() is a magic word that deletes any duplicates from the list.
            // If the user picked the same Goalkeeper 11 times, .Distinct() shrinks the list down to 1.  - AI explanataion-

            // Beside from this, I will implement a check in html that prevents showing the chosen player in the dropdown menu, but this is just a backup check.

            if (StarterIDs.Distinct().Count() != 11)
            {
                ModelState.AddModelError("", "You can not select the same player more than one position.");

                OnGet(); // Reload the dropdowns so the page doesn't break
                return Page(); // Stop saving and show the page again.
            }

            if (StarterIDs.Contains(0))
            {
                ModelState.AddModelError("", "You must select a player for every position.");
                OnGet();
                return Page();
            }

            //saving the tactics

            var team = teamManager.GetUserControlledTeam();
            team.Tactics = SelectedTactic;
            teamManager.UpdateTheTeam(team);


            // save the players
            var allTeamPlayers = playerManager.GetPlayersByTeamID(team.TeamID);

            foreach (var player in allTeamPlayers)
            {
                // if the player's id is inside the starterId, ...
                if (StarterIDs.Contains(player.PlayerID))
                {
                    player.IsStarting = true;
                }
                else
                {
                    player.IsStarting = false;
                }
                playerManager.UpdatePlayerStartingStatus(player.PlayerID, player.IsStarting);
            }
            return RedirectToPage();
        }

    }
}
