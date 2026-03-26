using System;               
using FarmersLeague.ML;     
using FarmersLeague.DL;     

namespace FarmersLeague.BL
{
    public class PlayerManager
    {
        private PlayerDb _playerDb = new PlayerDb();

        public void CreateNewPlayer(Player newPlayer) 
        {
            //my rules for creating a new player

            if (string.IsNullOrWhiteSpace(newPlayer.Name))
            {
                throw new Exception("Error: A player must have a name!");
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


            if (newPlayer.Position != "Forward" && newPlayer.Position != "Midfielder" && newPlayer.Position != "Defender" && newPlayer.Position != "Goalkeeper")
            {
                throw new Exception("Error: Position must be Forward, Midfielder, Defender, or Goalkeeper.");
            }

            // if the code made it here, this adds the player to do db
            _playerDb.AddPlayer(newPlayer);
        }
    }
}