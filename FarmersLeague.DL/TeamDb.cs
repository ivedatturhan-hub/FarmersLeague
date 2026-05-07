using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using FarmersLeague.ML;

namespace FarmersLeague.DL
{
    public class TeamDb
    {
        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FarmersLeagueDB;Integrated Security=True;";


       
        //method for creating a new teamm
        public void CreateTeam(int leagueID, string teamName, decimal budget, int points, string tactics, bool isUserControlled)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Team (LeagueID, TeamName, Budget, Points, Tactics, IsUserControlled) " +
                       "VALUES (@LeagueID, @TeamName, @Budget, @Points, @Tactics, @IsUserControlled)";

                SqlCommand command = new SqlCommand(query, connection);

                // 2. THE PARAMETERS: We just pass the exact lowercase variables from the top line directly in.
                // No more 'newTeam.' because the box is already unpacked!
                command.Parameters.AddWithValue("@LeagueID", leagueID);
                command.Parameters.AddWithValue("@TeamName", teamName);
                command.Parameters.AddWithValue("@Budget", budget);
                command.Parameters.AddWithValue("@Points", points);
                command.Parameters.AddWithValue("@Tactics", tactics);
                command.Parameters.AddWithValue("@IsUserControlled", isUserControlled);

                command.ExecuteNonQuery();



            }
        }


        // method for getting all teams from the db for the admin
        public List<AdminTeamDTO> GetAllTeamsForAdmin()
        {
            List<AdminTeamDTO> adminTeamList = new List<AdminTeamDTO>(); //creating a list to hold the teams we get

            using (SqlConnection connection = new SqlConnection(_connectionString))  //connecting to the database
            {
                string query = "SELECT * FROM Team";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Pack the big Admin object!
                    AdminTeamDTO teamAdmin = new AdminTeamDTO();

                    // We pull out every single stat and convert them
                    teamAdmin.TeamID = Convert.ToInt32(reader["TeamID"]);
                    teamAdmin.LeagueID = Convert.ToInt32(reader["LeagueID"]);
                    teamAdmin.TeamName = reader["TeamName"].ToString();
                    teamAdmin.Budget = Convert.ToDouble(reader["Budget"]);
                    teamAdmin.Points = Convert.ToInt32(reader["Points"]);
                    teamAdmin.Tactics = reader["Tactics"].ToString();
                    teamAdmin.IsUserControlled = Convert.ToBoolean(reader["IsUserControlled"]);

                    // Add it to the truck
                    adminTeamList.Add(teamAdmin);
                }
            }

            return adminTeamList;
        }




        // method for deleting a team from the db for the admin

        public void DeleteTeam(int teamID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Team WHERE TeamID = @TeamID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeamID", teamID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    
}