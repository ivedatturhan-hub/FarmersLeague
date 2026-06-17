using System;

namespace FarmersLeague.ML.Services 
{
    public class MatchManager
    {
        // for now it is just fake data, later i will get real data from the db
        public string MatchSimulation(Team hostTeam, List<Player> hostLineup, Team visitorTeam, List<Player> visitorLineup)
        {
            // retrieving the team's overall attack and defence stats from the team class.
            int hostTeam_Attack = hostTeam.GetOverallAttack(hostLineup);
            int hostTeam_Defence = hostTeam.GetOverallDefence(hostLineup);

            int visitorTeam_Attack = visitorTeam.GetOverallAttack(visitorLineup);
            int visitorTeam_Defence = visitorTeam.GetOverallDefence(visitorLineup);

            //Match Logic

            // every team will have 2 chances in a game in the pocket.
            int baseChances = 2;

            // calcuating how strong each team's attack is compared to other team's defence.
            // for every 5 points of advantage in attack vs defence, the team gets 1 extra chance. same logic works opposite if the team has a disadvantage also.
            int HostTeam_Advantage = (hostTeam_Attack - visitorTeam_Defence) / 5;
            int HostTeam_TotalChances = baseChances + HostTeam_Advantage;

            // for the visitor team.
            int VisitorTeam_Advantage = (visitorTeam_Attack - hostTeam_Defence) / 5;
            int VisitorTeam_TotalChances = baseChances + VisitorTeam_Advantage;

            if (HostTeam_TotalChances < 0) HostTeam_TotalChances = 0;
            if (VisitorTeam_TotalChances < 0) VisitorTeam_TotalChances = 0;


            // xG logic         


            int HomeTeam_Goals = 0;
            int VisitorTeam_Goals = 0;

            Random xG = new Random();

            // for every chance the team has, system will generate a random number between 0 and 100, if the number is less than 30, the team scores a goal.
            for (int i = 0; i < HostTeam_TotalChances; i++)
            {
                int opportunity = xG.Next(0, 101);
                if (opportunity < 30)
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

            return $"{hostTeam.TeamName} {HomeTeam_Goals}  -  {VisitorTeam_Goals} {visitorTeam.TeamName}";


        }
    }
}