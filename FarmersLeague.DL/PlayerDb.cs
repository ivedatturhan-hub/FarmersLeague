using Microsoft.Data.SqlClient; 
using FarmersLeague.ML;
using System.Collections.Generic;
using FarmersLeague.DL.DTO;

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

    }
}