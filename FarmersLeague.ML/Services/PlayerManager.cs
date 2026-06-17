
using FarmersLeague.ML.Interfaces;
using System;
using FarmersLeague.ML.DTOs;

namespace FarmersLeague.ML.Services
{
    public class PlayerManager
    {
        // creating private variables, but using interface, not the real database
        private IPlayerDb playerDb;
        private ITeamDb teamDb;

        // just like the player class, i have a constructor for the player manager for creating a manager. I need to choose which db i use.
        public PlayerManager(IPlayerDb PlayerDb, ITeamDb TeamDb)
        {
            playerDb = PlayerDb;
            teamDb = TeamDb;
        }



        // method for creating a new player
        public void CreateNewPlayer(AdminPlayerDTO newPlayer)
        {
            //translating string position to enum position for the player class
            Player.Positions smartPosition = (Player.Positions)Enum.Parse(typeof(Player.Positions), newPlayer.Position);

            // using the constructor of the player class to create a new player with the data from the dto.

            Player NewPlayer = new Player(newPlayer.Name, newPlayer.Age, smartPosition, newPlayer.BaseAttack, newPlayer.BaseDefence, newPlayer.MarketValue, newPlayer.Composure, newPlayer.Aggression);



            //my rules for creating a new player

            if (string.IsNullOrWhiteSpace(newPlayer.Name))
            {
                throw new Exception("Error: A player must have a name!"); //throw stops the code no matter what, and shows the message in the brackets
            }


            if (newPlayer.BaseAttack < 0 || newPlayer.BaseAttack > 99)
            {
                throw new Exception("Error: Attack rating must be between 0 and 99.");
            }


            if (newPlayer.BaseDefence < 0 || newPlayer.BaseDefence > 99)
            {
                throw new Exception("Error: Defence rating must be between 0 and 99.");
            }


            if (newPlayer.MarketValue < 0)
            {
                throw new Exception("Error: Market value cannot be negative.");
            }


            if (newPlayer.Condition < 0 || newPlayer.Condition > 100)
            {
                throw new Exception("Error: Condition must be between 0 and 100.");
            }


            if (newPlayer.Happiness < 0 || newPlayer.Happiness > 100)
            {
                throw new Exception("Error: Happiness must be between 0 and 100.");
            }


            if (newPlayer.Composure < 0 || newPlayer.Composure > 100)
            {
                throw new Exception("Error: Composure must be between 0 and 100.");
            }


            if (newPlayer.Aggression < 0 || newPlayer.Aggression > 100)
            {
                throw new Exception("Error: Aggression must be between 0 and 100.");
            }

            if (!Enum.IsDefined(typeof(Player.Positions), smartPosition))
            {
                throw new Exception("Error: You must select a valid player position.");
            }



            // if the code faced no problem, adds the player
            playerDb.AddPlayer(NewPlayer);
        }

        // method to get all the players in the database for the admin page.

        public List<AdminPlayerDTO> GetAllPlayersForAdmin()
        {
            return playerDb.GetAllPlayersForAdmin();
        }

        // method for getting all available players (for the transfer market page)
        public List<AdminPlayerDTO> GetPlayersWithNoTeam()
        {
            return playerDb.GetPlayersWithNoTeam();
        }


        // method for deleting a player from the database.

        public void DeletePlayer(int playerID)
        {
            // add rules here later 

            playerDb.DeletePlayer(playerID);
        }

        // method for updating a player
        public void UpdatePlayer(AdminPlayerDTO updatedPlayer)
        {
            if (string.IsNullOrWhiteSpace(updatedPlayer.Name))
            {
                throw new Exception("Error: A player must have a name!"); //throw stops the code no matter what, and shows the message in the brackets
            }


            if (updatedPlayer.BaseAttack < 0 || updatedPlayer.BaseAttack > 99)
            {
                throw new Exception("Error: Attack rating must be between 0 and 99.");
            }


            if (updatedPlayer.BaseDefence < 0 || updatedPlayer.BaseDefence > 99)
            {
                throw new Exception("Error: Defence rating must be between 0 and 99.");
            }


            if (updatedPlayer.MarketValue < 0)
            {
                throw new Exception("Error: Market value cannot be negative.");
            }


            if (updatedPlayer.Condition < 0 || updatedPlayer.Condition > 100)
            {
                throw new Exception("Error: Condition must be between 0 and 100.");
            }


            if (updatedPlayer.Happiness < 0 || updatedPlayer.Happiness > 100)
            {
                throw new Exception("Error: Happiness must be between 0 and 100.");
            }


            if (updatedPlayer.Composure < 0 || updatedPlayer.Composure > 100)
            {
                throw new Exception("Error: Composure must be between 0 and 100.");
            }


            if (updatedPlayer.Aggression < 0 || updatedPlayer.Aggression > 100)
            {
                throw new Exception("Error: Aggression must be between 0 and 100.");
            }

            if (!Enum.IsDefined(typeof(Player.Positions), updatedPlayer.Position))
            {
                throw new Exception("Error: You must select a valid player position.");
            }


            playerDb.UpdatePlayer(updatedPlayer);

        }

        // method for getting a player by id
        public AdminPlayerDTO GetPlayerById(int playerId)
        {
            return playerDb.GetPlayerByID(playerId);
        }

        // method for getting players in a team by team id
        public List<AdminPlayerDTO> GetPlayersByTeamID(int teamId)
        {

            // add rules here later
            return playerDb.GetPlayersByTeamID(teamId);
        }

        //method for changing a player's team
        public void UpdatePlayerTeam(int playerID, int teamID)
        {
            //update the remaining team budget after a transfer
            var player = playerDb.GetPlayerByID(playerID);
            var team = teamDb.GetTeamByID(teamID);

            double remainingBudget = team.Budget - player.MarketValue;

            if (remainingBudget < 0)
            {
                // Throwing an exception completely cancels the transaction.
                throw new Exception("Transfer failed: The team does not have enough budget for this player.");
            }

            if (player.TeamID !=0 )
            {
                throw new Exception("Transfer failed: The player is already in a team. Please remove the player from their current team before transferring.");
            }

            teamDb.UpdateTeamBudget(teamID, remainingBudget);

            playerDb.ChangePlayerTeam(playerID, teamID);
        }


        // method for removing a player from a team
        public void RemovePlayerFromTeam(int playerID, int teamID)
        {
            var player = playerDb.GetPlayerByID(playerID);
            var team = teamDb.GetTeamByID(teamID);

            double newBudget = team.Budget + player.MarketValue;
            // add rules here later

            teamDb.UpdateTeamBudget(teamID, newBudget);
            playerDb.RemovePlayerFromTeam(playerID);

        }

        // method for updating a player's starting status
        public void UpdatePlayerStartingStatus(int playerId, bool isStarting)
        {
            playerDb.UpdatePlayerStartingStatus(playerId, isStarting);
        }
    }
}