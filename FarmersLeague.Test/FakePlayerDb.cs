using FarmersLeague.ML;
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
        private AdminPlayerDTO fakePlayer2;

        private List<AdminPlayerDTO> fakePlayerList = new List<AdminPlayerDTO>();


        //constructor to create a fake player db
        public FakePlayerDb(AdminPlayerDTO FakePlayer)
        {
            fakePlayer = FakePlayer;
        }


        // constructor to create a list with two players, so we can test the get all players method in the player manager
        public FakePlayerDb(AdminPlayerDTO FakePlayer, AdminPlayerDTO FakePlayer2)
        {
            fakePlayerList.Add(FakePlayer);
            fakePlayerList.Add(FakePlayer2);
        }

        // constructor with zero players so i can test a team without having to add players to the fake db
        public FakePlayerDb()
        {

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
            return fakePlayerList;
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

        public void AddPlayer(Player NewPlayer)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerStartingStatus(int playerId, bool isStarting)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetStartingLineup(int teamID)
        {
            throw new NotImplementedException();
        }
    }
}
