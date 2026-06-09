using FarmersLeague.ML.DTOs;
using FarmersLeague.ML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmersLeague.Test
{
    public class FakePlayerDb : IPlayerDb
    {

        private AdminPlayerDTO fakePlayer;


        //constructor to create a fake player
        public FakePlayerDb(AdminPlayerDTO FakePlayer)
        {
            fakePlayer = FakePlayer;
        }






        // The manager will call this, so we hand back a player who costs 100 million
        public AdminPlayerDTO GetPlayerByID(int id)
        {
            return fakePlayer;
        }



        public void AddPlayer(string Name, int Age, string Position, int BaseAttack, int BaseDefence, double MarketValue, bool IsAvaible, int Condition, int Happiness, int Composure, int Aggression, int SeasonGoals, int SeasonAssists, int YellowCards, int RedCards)
        {
            throw new NotImplementedException();
        }

        public List<AdminPlayerDTO> GetAllPlayersForAdmin()
        {
            throw new NotImplementedException();
        }

        public List<AdminPlayerDTO> GetPlayersWithNoTeam()
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayer(AdminPlayerDTO updatedPlayer)
        {
            throw new NotImplementedException();
        }

        public List<AdminPlayerDTO> GetPlayersByTeamID(int teamID)
        {
            throw new NotImplementedException();
        }

        public void RemovePlayerFromTeam(int playerID)
        {
            throw new NotImplementedException();
        }

        public void DeletePlayer(int playerID)
        {
            throw new NotImplementedException();
        }

        public void ChangePlayerTeam(int playerID, int teamID)
        {

        }
    }
}
