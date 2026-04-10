using Microsoft.Data.SqlClient;
using FarmersLeague.ML;
using System.Collections.Generic;


namespace FarmersLeague.DL
{
    public class TeamDb
    {
        private string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FarmersLeagueDB;Integrated Security=True;";

        public List<Team> GetAllTeams()
        {   
            List<Team> teamList = new List<Team>(); //creating a list to hold the teams we get

            using (SqlConnection connection = new SqlConnection(connectionString))  //connecting to the database
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
      
    }

    
}