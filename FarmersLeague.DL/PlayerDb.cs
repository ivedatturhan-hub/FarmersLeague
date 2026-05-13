    using FarmersLeague.DL.DTO;
using FarmersLeague.ML;
using Microsoft.Data.SqlClient; 
using System.Collections.Generic;
using System.Numerics;

namespace FarmersLeague.DL
{
    public class PlayerDb
    {
        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FarmersLeagueDB;Integrated Security=True;";

        //my method to add a player to the database
        public void AddPlayer(string Name, int Age, string Position, int BaseAttack, int BaseDefence, decimal MarketValue,
             bool IsAvailable, int Condition, int Happiness, int Composure, int Aggression, int SeasonGoals, int SeasonAssists, int YellowCards, int RedCards)
        {
            // the sql query. I match @ placeholders to properties of player class like a bridge for security i assume.
            string query = "INSERT INTO Player (Name, Age, Position, BaseAttack, BaseDefence, MarketValue, IsAvailable, Condition, Happiness, Composure, Aggression, SeasonGoals, SeasonAssists, YellowCards, RedCards) " +
                           "VALUES (@Name, @Age, @Position, @BaseAttack, @BaseDefence, @MarketValue, 1, @Condition, @Happiness, @Composure, @Aggression, @SeasonGoals, @SeasonAssists, @YellowCards, @RedCards)";

            // i connect my database using the connection string i made above. 
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // the query we matched with the @ placeholders is now a command that can be sent to the database. 
                SqlCommand command = new SqlCommand(query, connection);

                // now i swap the placeholders with actual values from database.  

                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Age", Age);
                command.Parameters.AddWithValue("@Position", Position);
                command.Parameters.AddWithValue("@BaseAttack", BaseAttack);
                command.Parameters.AddWithValue("@BaseDefence", BaseDefence);
                command.Parameters.AddWithValue("@MarketValue", MarketValue);
                command.Parameters.AddWithValue("@IsAvailable", IsAvailable);
                command.Parameters.AddWithValue("@Condition", Condition);
                command.Parameters.AddWithValue("@Happiness", Happiness);
                command.Parameters.AddWithValue("@Composure", Composure);
                command.Parameters.AddWithValue("@Aggression", Aggression);
                command.Parameters.AddWithValue("@SeasonGoals", SeasonGoals);
                command.Parameters.AddWithValue("@SeasonAssists", SeasonAssists);
                command.Parameters.AddWithValue("@YellowCards", YellowCards);
                command.Parameters.AddWithValue("@RedCards", RedCards);


                connection.Open();
                command.ExecuteNonQuery();
            }
        }



        // method for getting all the players from the database for the admin.

        public List<AdminPlayerDTO> GetAllPlayersForAdmin()
        {
            List<AdminPlayerDTO> adminPlayerList = new List<AdminPlayerDTO>(); //creating a list to hold the players we get from the database.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Player";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AdminPlayerDTO playerAdmin = new AdminPlayerDTO();

                    playerAdmin.PlayerID = Convert.ToInt32(reader["PlayerID"]);
                    playerAdmin.Name = reader["Name"].ToString();
                    playerAdmin.Age = Convert.ToInt32(reader["Age"]);
                    playerAdmin.Position = reader["Position"].ToString();
                    playerAdmin.BaseAttack = Convert.ToInt32(reader["BaseAttack"]);
                    playerAdmin.BaseDefence = Convert.ToInt32(reader["BaseDefence"]);
                    playerAdmin.MarketValue = (double)Convert.ToDecimal(reader["MarketValue"]);
                    playerAdmin.IsAvailable = Convert.ToBoolean(reader["IsAvailable"]);
                    playerAdmin.Condition = Convert.ToInt32(reader["Condition"]);
                    playerAdmin.Happiness = Convert.ToInt32(reader["Happiness"]);
                    playerAdmin.Composure = Convert.ToInt32(reader["Composure"]);
                    playerAdmin.Aggression = Convert.ToInt32(reader["Aggression"]);
                    playerAdmin.SeasonGoals = Convert.ToInt32(reader["SeasonGoals"]);
                    playerAdmin.SeasonAssists = Convert.ToInt32(reader["SeasonAssists"]);
                    playerAdmin.YellowCards = Convert.ToInt32(reader["YellowCards"]);
                    playerAdmin.RedCards = Convert.ToInt32(reader["RedCards"]);

                    // add it to the list we made above.
                    adminPlayerList.Add(playerAdmin);

                }
            }
            return adminPlayerList; // return the list of players to the admin.
        }



        // method for getting all the players with no team, this will be used in the assign players page to show available players
        public List<AdminPlayerDTO> GetPlayersWithNoTeam()
        {
            List<AdminPlayerDTO> playersWithNoTeam = new List<AdminPlayerDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // getting the players with NO team.
                string query = "SELECT * FROM Player WHERE TeamID IS NULL";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AdminPlayerDTO freePlayer = new AdminPlayerDTO();

                        freePlayer.PlayerID = Convert.ToInt32(reader["PlayerID"]);
                        freePlayer.Name = reader["Name"].ToString();
                        freePlayer.MarketValue = (double)Convert.ToDecimal(reader["MarketValue"]);
                        freePlayer.Position = reader["Position"].ToString();

                        playersWithNoTeam.Add(freePlayer);

                    }
                }
                    
            }
            return playersWithNoTeam;
        }


        // Method for deleting a player from the db

        public void DeletePlayer(int playerID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Player WHERE PlayerID = @PlayerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlayerID", playerID);
                connection.Open();
                command.ExecuteNonQuery();
            }

        }




        // method for editing the player data

        public void UpdatePlayer(AdminPlayerDTO updatedPlayer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // the sql command to update the spesific player data for a spesific id
                string query = @"UPDATE Player
                                    SET Name = @Name, Age = @Age, Position = @Position, MarketValue = @MarketValue, BaseAttack = @BaseAttack, BaseDefence = @BaseDefence,
                                    Composure = @Composure, Aggression = @Aggression
                                 WHERE PlayerID = @PlayerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", updatedPlayer.Name);
                command.Parameters.AddWithValue("@Age", updatedPlayer.Age);
                command.Parameters.AddWithValue("@Position", updatedPlayer.Position);
                command.Parameters.AddWithValue("@MarketValue", updatedPlayer.MarketValue);
                command.Parameters.AddWithValue("@BaseAttack", updatedPlayer.BaseAttack);
                command.Parameters.AddWithValue("@BaseDefence", updatedPlayer.BaseDefence);
                command.Parameters.AddWithValue("@Composure", updatedPlayer.Composure);
                command.Parameters.AddWithValue("@Aggression", updatedPlayer.Aggression);
                command.Parameters.AddWithValue("@PlayerID", updatedPlayer.PlayerID); // this is to find the player, not to change the id

                command.ExecuteNonQuery();
            }
        }




        // method for getting player data when editing a player, so we can show the current data in the edit page

        public AdminPlayerDTO GetPlayerByID(int playerID)
        {
            AdminPlayerDTO editedPlayer = new AdminPlayerDTO();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // getting the row with the spesific id
                string query = "SELECT * FROM Player WHERE PlayerID = @PlayerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlayerID", playerID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // if the db finds the player, it fills the data to the form
                    if (reader.Read())
                    {
                        editedPlayer.PlayerID = Convert.ToInt32(reader["PlayerID"]);
                        editedPlayer.Name = reader["Name"].ToString();
                        editedPlayer.Age = Convert.ToInt32(reader["Age"]);
                        editedPlayer.Position = reader["Position"].ToString();
                        editedPlayer.MarketValue = (double)Convert.ToDecimal(reader["MarketValue"]);
                        editedPlayer.BaseAttack = Convert.ToInt32(reader["BaseAttack"]);
                        editedPlayer.BaseDefence = Convert.ToInt32(reader["BaseDefence"]);
                    }
                }

            }
            return editedPlayer; // return the player data to the edit page.
        }




        // method for getting all the players by their team id. This will allow us to show the players in a team

        public List<AdminPlayerDTO> GetPlayersByTeamID(int teamId)
        {
            List<AdminPlayerDTO> teamSquad = new List<AdminPlayerDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                //  We only want players with the matching TeamID.
                string query = "SELECT * FROM Player WHERE TeamID = @TeamID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TeamID", teamId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AdminPlayerDTO player = new AdminPlayerDTO();

                    player.PlayerID = Convert.ToInt32(reader["PlayerID"]);
                    player.Name = reader["Name"].ToString();
                    player.Age = Convert.ToInt32(reader["Age"]);
                    player.Position = reader["Position"].ToString();
                    player.MarketValue = (double)Convert.ToDecimal(reader["MarketValue"]);
                    player.BaseAttack = Convert.ToInt32(reader["BaseAttack"]);
                    player.BaseDefence = Convert.ToInt32(reader["BaseDefence"]);

                    teamSquad.Add(player);
                }
            }

            return teamSquad;
        }


        // method for changing player's team
        public void ChangePlayerTeam(int playerID, int teamID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // updating only the teamID column for this spesific player with the matching playerID.
                string query = "UPDATE Player SET TeamID = @TeamID WHERE PlayerID = @PlayerID";

                SqlCommand command = new SqlCommand(query, connection);

                // Securely attach our parameters
                command.Parameters.AddWithValue("@TeamID", teamID);
                command.Parameters.AddWithValue("@PlayerID", playerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // method for removing a player from a team
        public void RemovePlayerFromTeam(int playerID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Player SET TeamID = NULL WHERE PlayerID = @PlayerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlayerID", playerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
            

    }

}
    
