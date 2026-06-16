using System;

namespace FarmersLeague.ML.Services 
{
    public class MatchManager
    {
        // for now it is just fake data, later i will get real data from the db
        public void CalculateChances(int HostTeam_Attack, int HostTeam_Defence, int VisitorTeam_Attack, int VisitorTeam_Defence)
        {
            // every team will have 2 chances in a game in the pocket.
            int baseChances = 2;

            // calcuating how strong each team's attack is compared to other team's defence.

            // for every 5 points of advantage in attack vs defence, the team gets 1 extra chance. same logic goes for if the team has a disadvantage also.
            int HostTeam_Advantage = (HostTeam_Attack - VisitorTeam_Defence) / 5;
            int HostTeam_TotalChances = baseChances + HostTeam_Advantage;

            // for the visitor team.
            int VisitorTeam_Advantage = (VisitorTeam_Attack - HostTeam_Defence) / 5;
            int VisitorTeam_TotalChances = baseChances + VisitorTeam_Advantage;

            if (HostTeam_TotalChances < 0) HostTeam_Advantage = 0;
            if (VisitorTeam_TotalChances < 0) VisitorTeam_Advantage = 0;









        }
    }
}