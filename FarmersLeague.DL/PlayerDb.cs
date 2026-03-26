using Microsoft.Data.SqlClient; 
using FarmersLeague.ML;      

namespace FarmersLeague.DL
{
    public class PlayerDb
    {
        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FarmersLeagueDB;Integrated Security=True;";

        //my method to add a player to the database
        public void AddPlayer(Player newPlayer)
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
                //Why do I need to use AddWithValue? I will ask this
                command.Parameters.AddWithValue("@Name", newPlayer.Name);
                command.Parameters.AddWithValue("@PlayerID", newPlayer.PlayerID);
                command.Parameters.AddWithValue("@Age", newPlayer.Age);
                command.Parameters.AddWithValue("@Position", newPlayer.Position);
                command.Parameters.AddWithValue("@BaseAttack", newPlayer.BaseAttack);
                command.Parameters.AddWithValue("@BaseDefence", newPlayer.BaseDefence);
                command.Parameters.AddWithValue("@MarketValue", newPlayer.MarketValue);   
                command.Parameters.AddWithValue("@IsAvailable", newPlayer.IsAvailable); 
                command.Parameters.AddWithValue("@Condition", newPlayer.Condition); 
                command.Parameters.AddWithValue("@Happiness", newPlayer.Happiness);
                command.Parameters.AddWithValue("@Composure", newPlayer.Composure);
                command.Parameters.AddWithValue("@Aggression", newPlayer.Aggression);
                command.Parameters.AddWithValue("@SeasonGoals", newPlayer.SeasonGoals);
                command.Parameters.AddWithValue("@SeasonAssists", newPlayer.SeasonAssists);
                command.Parameters.AddWithValue("@YellowCards", newPlayer.YellowCards);
                command.Parameters.AddWithValue("@RedCards", newPlayer.RedCards);


          
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}