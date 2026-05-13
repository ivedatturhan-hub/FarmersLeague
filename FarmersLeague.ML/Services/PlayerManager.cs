using System;
using FarmersLeague.DL;
using FarmersLeague.DL.DTO;

namespace FarmersLeague.ML.Services
{
    public class PlayerManager
    {
        private PlayerDb playerDb = new PlayerDb();


        // method for creating a new player
        public void CreateNewPlayer(Player newPlayer)
        {
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

            if (!Enum.IsDefined(typeof(Player.Positions), newPlayer.Position))
            {
                throw new Exception("Error: You must select a valid player position.");
            }



            // if the code faced no problem, adds the player
            playerDb.AddPlayer(newPlayer.Name, newPlayer.Age, newPlayer.Position.ToString(), newPlayer.BaseAttack, newPlayer.BaseDefence, Convert.ToDecimal(newPlayer.MarketValue),
                newPlayer.IsAvailable, newPlayer.Condition, newPlayer.Happiness, newPlayer.Composure, newPlayer.Aggression, newPlayer.SeasonGoals, newPlayer.SeasonAssists, newPlayer.YellowCards, newPlayer.RedCards);
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
        public void UpdateThePlayer(AdminPlayerDTO updatedPlayer)
        {
            // add the same rules as in create new player here later
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
            // add rules here later (does adding a player exceed the team limit?)
            playerDb.ChangePlayerTeam(playerID, teamID);
        }


        // method for removing a player from a team
        public void RemovePlayerFromTeam(int playerID)
        {
            // add rules here later
            playerDb.RemovePlayerFromTeam(playerID);

        }
    }
}