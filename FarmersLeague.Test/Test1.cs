using FarmersLeague.ML;
using FarmersLeague.ML.DTOs;
using FarmersLeague.ML.Interfaces;
using FarmersLeague.ML.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FarmersLeague.Test;
namespace FarmersLeague.Tests
{
    [TestClass]
    public class PlayerManagerTests
    {
        [TestMethod]
        public void UpdatePlayerTeam_If_Budget_Exceeded()
        {
            // arrange part
            // creating a fake team and a player by using the constructor i made in the fake team db below the test method.
            AdminTeamDTO poorTeam = new AdminTeamDTO { TeamID = 1, Budget = 50 }; // team with only 50 million euros
            AdminPlayerDTO expensivePlayer = new AdminPlayerDTO { PlayerID = 1, MarketValue = 100 }; // player worth 100 million euros

            ITeamDb fakeTeamDb = new FakeTeamDb(poorTeam);
            IPlayerDb fakePlayerDb = new FakePlayerDb(expensivePlayer);





            // building the manager with the fake dbs
            PlayerManager manager = new PlayerManager(fakePlayerDb, fakeTeamDb);

            // act and assert part

            Assert.ThrowsException<Exception>(() =>
            {
                manager.UpdatePlayerTeam(1, 1);
            });
        }


        [TestMethod]
        public void BudgetUpdateTest()
        {
            // arrange part
            // creating a fake team and a player by using the constructors in the fake db

            AdminTeamDTO testingTeam = new AdminTeamDTO { TeamID = 2, Budget = 100 };
            AdminPlayerDTO testingPlayer = new AdminPlayerDTO { PlayerID = 2, MarketValue = 40 };


            ITeamDb testingTeamDb = new FakeTeamDb(testingTeam);
            IPlayerDb testingPlayerDb = new FakePlayerDb(testingPlayer);

            double expectedUpdatedBudget = 60;

            // building the manager with fake dbs

            TeamManager teamManager = new TeamManager(testingPlayerDb, testingTeamDb);
            PlayerManager playerManager = new PlayerManager(testingPlayerDb, testingTeamDb);

            //act
            playerManager.UpdatePlayerTeam(2, 2);
            double newBudget = testingTeamDb.GetTeamByID(2).Budget;


            // assert
            Assert.AreEqual(expectedUpdatedBudget, newBudget);
        }



        [TestMethod]
        public void TransferPlayer_toThe_sameTeam()
        {
            //arrange part

            AdminTeamDTO testingTeam = new AdminTeamDTO { TeamID = 3, Budget = 100 };
            AdminPlayerDTO testingPlayer = new AdminPlayerDTO { PlayerID = 3, TeamID = 3, MarketValue = 40 };

            ITeamDb testingTeamDb = new FakeTeamDb(testingTeam);
            IPlayerDb testingPlayerDb = new FakePlayerDb(testingPlayer);

            PlayerManager playerManager = new PlayerManager(testingPlayerDb, testingTeamDb);


            // act and assert

            Assert.ThrowsException<Exception>(() =>
            {
                playerManager.UpdatePlayerTeam(3, 3);
            });


        }
    }
}



    