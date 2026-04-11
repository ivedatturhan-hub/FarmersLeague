using Microsoft.Data.SqlClient;
using FarmersLeague.ML;
using System.Collections.Generic;


namespace FarmersLeague.DL
{
    public class TeamDb
    {
        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FarmersLeagueDB;Integrated Security=True;";

        // method for getting all teams from the db
        public List<Team> GetAllTeams()
        {   
            List<Team> teamList = new List<Team>(); //creating a list to hold the teams we get

            using (SqlConnection connection = new SqlConnection(_connectionString))  //connecting to the database
            {
                string query = "SELECT * FROM Team";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int teamID =  (int)reader["TeamID"];
                    int leagueID = (int)reader["LeagueID"];
                    string teamName = (string)reader["TeamName"];
                    decimal budget = (decimal)reader["Budget"];
                    int points = (int)reader["Points"];
                    string tacticsString = (string)reader["Tactics"];
                    bool isControlled = (bool)reader["IsUserControlled"];

                    // pushing the data we got from the db into the team constructor to create a team object
                    Team t = new Team(teamID, leagueID, teamName, budget, points, tacticsString, isControlled);

                }

            }
            return teamList;
        }


        //method for creating a new teamm
        public void CreateTeam(Team newTeam)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Team (LeagueID, TeamName, Budget, Points, Tactics, IsUserControlled)
                VALUES (@LeagueID, @TeamName, @Budget, @Points, @Tactics, @IsUserControlled )";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LeagueID", newTeam.LeagueID);
                command.Parameters.AddWithValue("@TeamName", newTeam.TeamName);
                command.Parameters.AddWithValue("@Budget", newTeam.Budget);
                command.Parameters.AddWithValue("@Points", newTeam.Points);
                command.Parameters.AddWithValue("@Tactics", newTeam.Tactics.ToString());  //converting the enum to a string to store in the db bc db expects a string
                command.Parameters.AddWithValue("@IsUserControlled", newTeam.IsUserControlled);

                command.ExecuteNonQuery();



            }
        }
      
    }

    
}