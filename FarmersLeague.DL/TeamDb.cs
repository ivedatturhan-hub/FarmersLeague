using FarmersLeague.ML.DTOs;
using FarmersLeague.ML;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Numerics;
using FarmersLeague.ML.Interfaces;

namespace FarmersLeague.DL
{
    public class TeamDb : ITeamDb
    {
        private string _connectionString = "Server=localhost\\SQLEXPRESS;Database=FarmersLeague;Trusted_Connection=True;TrustServerCertificate=True;";

        //method for creating a new teamm
        public void CreateTeam(int leagueID, string teamName, double budget, int points, string tactics, bool isUserControlled)
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


        //method for editing the team data

        public void UpdateTeam(AdminTeamDTO updatedTeam)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Team SET TeamName = @TeamName, Budget = @Budget, Points = @Points WHERE TeamID = @TeamID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeamName", updatedTeam.TeamName);
                command.Parameters.AddWithValue("@Budget", updatedTeam.Budget);
                command.Parameters.AddWithValue("@Points", updatedTeam.Points);
                command.Parameters.AddWithValue("@TeamID", updatedTeam.TeamID); // this is to find the team, not to update it!
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //method for getting a team by its ID, so we can show the current data in the edit page before we update it.

        public AdminTeamDTO GetTeamByID(int teamID)
        {
            AdminTeamDTO editedTeam = new AdminTeamDTO();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Team WHERE TeamID = @TeamID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeamID", teamID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        editedTeam.TeamID = Convert.ToInt32(reader["TeamID"]);
                        editedTeam.LeagueID = Convert.ToInt32(reader["LeagueID"]);
                        editedTeam.TeamName = reader["TeamName"].ToString();
                        editedTeam.Budget = Convert.ToDouble(reader["Budget"]);
                        editedTeam.Points = Convert.ToInt32(reader["Points"]);
                        editedTeam.Tactics = reader["Tactics"].ToString();
                        editedTeam.IsUserControlled = Convert.ToBoolean(reader["IsUserControlled"]);
                    }
                }
                return editedTeam;
            }


        }

        //update the team budget after a transfer
        public void UpdateTeamBudget(int teamID, double newBudget)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Update just the budget column for the specific team
                string query = "UPDATE Team SET Budget = @NewBudget WHERE TeamID = @TeamID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NewBudget", newBudget);
                command.Parameters.AddWithValue("@TeamID", teamID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public AdminTeamDTO GetUserControlledTeam()
        {
            // i started with null instead of "new". If i did not do that, in any case that database could not find a team, it would return the team that i created anyway
            AdminTeamDTO userTeam = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Team WHERE IsUserControlled = 1";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // i create the team when database finds a team that is user controlled. 
                        userTeam = new AdminTeamDTO();

                        userTeam.TeamID = Convert.ToInt32(reader["TeamID"]);
                        userTeam.LeagueID = Convert.ToInt32(reader["LeagueID"]);
                        userTeam.TeamName = reader["TeamName"].ToString();
                        userTeam.Budget = Convert.ToDouble(reader["Budget"]);
                        userTeam.Points = Convert.ToInt32(reader["Points"]);
                        userTeam.Tactics = reader["Tactics"].ToString();
                        userTeam.IsUserControlled = Convert.ToBoolean(reader["IsUserControlled"]);
                    }
                }
            }
            return userTeam;
        }
    }
}