using System;
using FarmersLeague.ML;
using System.Collections.Generic;
using FarmersLeague.ML.Interfaces;
using FarmersLeague.ML.DTOs;


namespace FarmersLeague.ML.Services 
{
    public class MatchManager
    {
        private IPlayerDb playerDb;
        private ITeamDb teamDb;

        //creating a constructor

        public MatchManager(IPlayerDb PlayerDb, ITeamDb TeamDb)
        {
           playerDb = PlayerDb;
            teamDb = TeamDb;
        }

        // method to simulate a match between two teams
        public MatchReportDTO MatchSimulation(Team hostTeam, List<Player> hostLineup, Team visitorTeam, List<Player> visitorLineup)
        {
            // retrieving the team's overall attack and defence stats from the team class.
            int hostTeam_Attack = hostTeam.GetOverallAttack(hostLineup);
            int hostTeam_Defence = hostTeam.GetOverallDefence(hostLineup);

            int visitorTeam_Attack = visitorTeam.GetOverallAttack(visitorLineup);
            int visitorTeam_Defence = visitorTeam.GetOverallDefence(visitorLineup);



            //Match Logic

            // every team will have 2 chances in a game in the pocket.
            int baseChances = 6;

            // calcuating how strong each team's attack is compared to other team's defence.
            // for every 5 points of advantage in attack vs defence, the team gets 1 extra chance. same logic works opposite if the team has a disadvantage also.
            int HostTeam_Advantage = (hostTeam_Attack - visitorTeam_Defence) / 5;
            int HostTeam_TotalChances = baseChances + HostTeam_Advantage;

            // for the visitor team.
            int VisitorTeam_Advantage = (visitorTeam_Attack - hostTeam_Defence) / 5;
            int VisitorTeam_TotalChances = baseChances + VisitorTeam_Advantage;

            if (HostTeam_TotalChances < 0) HostTeam_TotalChances = 1;
            if (VisitorTeam_TotalChances < 0) VisitorTeam_TotalChances = 1;
            if (hostLineup.Count != 11)
            {
                throw new Exception($"{hostTeam.TeamName} cannot play because they do not have exactly 11 starting players! They currently have {hostLineup.Count}.");
            }
            if (visitorLineup.Count != 11)
            {
                throw new Exception($"{visitorTeam.TeamName} cannot play because they do not have exactly 11 starting players! They currently have {visitorLineup.Count}.");
            }


            // xG logic         


            int HomeTeam_Goals = 0;
            int VisitorTeam_Goals = 0;

            Random xG = new Random();

            // for every chance the team has, system will generate a random number between 0 and 100, if the number is less than 30, the team scores a goal.
            for (int i = 0; i < HostTeam_TotalChances; i++)
            {
                int opportunity = xG.Next(0, 101);
                if (opportunity < 25)
                {
                    HomeTeam_Goals++;
                }
            }


            for (int i = 0; i < VisitorTeam_TotalChances; i++)
            {
                int opportunity = xG.Next(0, 101);
                if (opportunity < 30)
                {
                    VisitorTeam_Goals++;
                }
            }


            //update the league points

            if (HomeTeam_Goals > VisitorTeam_Goals)
            {
                teamDb.UpdatePoints(hostTeam.TeamID, 3);
            }
        
            else if (HomeTeam_Goals < VisitorTeam_Goals)
            {
                teamDb.UpdatePoints(visitorTeam.TeamID, 3);
            }
            else
            {
                teamDb.UpdatePoints(hostTeam.TeamID, 1);
                teamDb.UpdatePoints(visitorTeam.TeamID, 1);
            }

            // match report
            MatchReportDTO report = new MatchReportDTO();
            report.FinalScore = $"{hostTeam.TeamName} {HomeTeam_Goals} - {VisitorTeam_Goals} {visitorTeam.TeamName}";

            report.HostAttack = hostTeam_Attack;
            report.HostDefence = hostTeam_Defence;
            report.HostChances = HostTeam_TotalChances;

            report.VisitorAttack = visitorTeam_Attack;
            report.VisitorDefence = visitorTeam_Defence;
            report.VisitorChances = VisitorTeam_TotalChances;

            report.HostPoints = hostTeam.Points;
            report.VisitorPoints = visitorTeam.Points;

            return report; 
        }
        
    }
}