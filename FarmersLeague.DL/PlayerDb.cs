using Microsoft.Data.SqlClient; // This brings in the SQL tools we just downloaded
using FarmersLeague.ML;       // This lets us use your Player class!

namespace FarmersLeague.DL
{
    public class PlayerDb
    {
        // We will store your connection string here so we don't have to type it every time
        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FarmersLeagueDB;Integrated Security=True;";

        // This method requires you to hand it a "Player" object
        public void AddPlayer(Player newPlayer)
        {
            // 1. Write the SQL query. Notice we use @ names as placeholders for security.
            string query = "INSERT INTO Player (Name, TeamID, PlayerID, Age, Position, BaseAttack, BaseDefence, MarketValue, IsStarting, IsAvailable, Condition, Happiness, Composure, Aggression, SeasonGoals, SeasonAssists, YellowCards, RedCards) " +
                           "VALUES (@Name, @TeamID, @PlayerID, @Age, @Position, @BaseAttack, @BaseDefence, @MarketValue, 1, 1, @Condition, @Happiness, @Composure, @Agression, @SeasonGoals, @SeasonAssists, @YellowCards, @RedCards)";

            // 2. Open the connection pipe to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // 3. Put the query and the connection together into a Command
                SqlCommand command = new SqlCommand(query, connection);

                // 4. Swap out the @ placeholders with the actual data from your ML object
                command.Parameters.AddWithValue("@Name", newPlayer.Name);
                command.Parameters.AddWithValue("@TeamID", newPlayer.TeamID);
                command.Parameters.AddWithValue("@PlayerID", newPlayer.PlayerID);
                command.Parameters.AddWithValue("@Age", newPlayer.Age);
                command.Parameters.AddWithValue("@Position", newPlayer.Position);
                command.Parameters.AddWithValue("@BaseAttack", newPlayer.BaseAttack);
                command.Parameters.AddWithValue("@BaseDefence", newPlayer.BaseDefence);
                command.Parameters.AddWithValue("@MarketValue", newPlayer.MarketValue);
                command.Parameters.AddWithValue("@IsStarting", newPlayer.IsStarting);   
                command.Parameters.AddWithValue("@IsAvailable", newPlayer.IsAvailable); 
                command.Parameters.AddWithValue("@Condition", newPlayer.Condition); 
                command.Parameters.AddWithValue("@Happiness", newPlayer.Happiness);
                command.Parameters.AddWithValue("@Composure", newPlayer.Composure);
                command.Parameters.AddWithValue("@Aggression", newPlayer.Aggression);
                command.Parameters.AddWithValue("@SeasonGoals", newPlayer.SeasonGoals);
                command.Parameters.AddWithValue("@SeasonAssists", newPlayer.SeasonAssists);
                command.Parameters.AddWithValue("@YellowCards", newPlayer.YellowCards);
                command.Parameters.AddWithValue("@RedCards", newPlayer.RedCards);


           

                // 5. Turn the pipe on, send the command, and turn it off automatically
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}