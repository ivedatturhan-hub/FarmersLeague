using FarmersLeague.DL;
using FarmersLeague.ML;
using FarmersLeague.ML.DTOs;
using FarmersLeague.ML.Interfaces;
using FarmersLeague.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace FarmersLeague.UI.Pages
{
    public class MatchSimulationModel : PageModel
    {
        // Property to hold the final score or error message to show on the HTML page
        public MatchReportDTO MatchResult { get; set; }

        // creating empty boxes for the manager and databases at the top so whole page can use it.
        MatchManager matchManager;
        IPlayerDb playerDb;
        ITeamDb teamDb;

        public MatchSimulationModel()
        {
            // building the specific databases I want to use in this page
            playerDb = new PlayerDb();
            teamDb = new TeamDb();

            // putting the manager in its box
            matchManager = new MatchManager(playerDb, teamDb);
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            try
            {
                AdminTeamDTO hostTeamDTO = teamDb.GetTeamByID(3);
                AdminTeamDTO visitorTeamDTO = teamDb.GetTeamByID(2);

                //converting the DTOs to the actual team objects the manager can use
                Team hostTeam = new Team(hostTeamDTO.TeamID, 1, hostTeamDTO.TeamName, hostTeamDTO.Budget, hostTeamDTO.Points, "Balanced", hostTeamDTO.IsUserControlled);
                Team visitorTeam = new Team(visitorTeamDTO.TeamID, 1, visitorTeamDTO.TeamName, visitorTeamDTO.Budget, visitorTeamDTO.Points, "Balanced", visitorTeamDTO.IsUserControlled);

                // getting the starting lineups for both teams from the database
                List<Player> hostLineup = playerDb.GetStartingLineup(3);
                List<Player> visitorLineup = playerDb.GetStartingLineup(2);

                // giving the manager the teams and lineups to simulate the match
                MatchResult = matchManager.MatchSimulation(hostTeam, hostLineup, visitorTeam, visitorLineup);

                // getting the teams again so it can display the current points
                AdminTeamDTO updatedHostTeamDTO = teamDb.GetTeamByID(3);
                AdminTeamDTO updatedVisitorTeamDTO = teamDb.GetTeamByID(2);
                MatchResult.HostPoints = updatedHostTeamDTO.Points;
                MatchResult.VisitorPoints = updatedVisitorTeamDTO.Points;
            }

                
            
            catch (Exception ex)
            {
                MatchResult = new MatchReportDTO();
                MatchResult.FinalScore = "Error simulating match: " + ex.Message;
            }
        }
    }
}