using System;
using RedGate.SQLDataGenerator.Engine.Generators;
using RedGate.SQLDataGenerator.Engine.Generators.Static;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Basic
{
    [Generator(typeof(int), "CallLogs", "CallLogsUserIdGenerator", "User Ids based on shift ids")]
    public class CallLogsUserIdGenerator : IGenerator
    {
        public CallLogsUserIdGenerator(GeneratorParameters parameters)
        {
        }

        public System.Collections.IEnumerator GetEnumerator(GenerationSession session)
        {
            using(SqlConnection conn = new SqlConnection(session.ConnectionProperties.ConnectionString))
            {
                conn.Open();
                Dictionary<int, List<int>> ShiftUsers = new Dictionary<int, List<int>>();
                SqlCommand cmd = new SqlCommand("SELECT TEAM_ID FROM SRC_VS_TEAMS", conn);
                // Load the list of teams
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShiftUsers[(int)reader["TEAM_ID"]] = new List<int>();
                    }
                }

                // Load the list of users in each team
                cmd = new SqlCommand("SELECT USER_ID, TEAM FROM SRC_VS_USERS", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShiftUsers[(int)reader["TEAM"]].Add((int)reader["USER_ID"]);
                    }
                }

                // Load the list of what team is in each shift
                Dictionary<int, int> shiftTeams = new Dictionary<int, int>();
                cmd = new SqlCommand("SELECT SHIFT_ID, TEAM FROM SRC_VS_TEAM_SHIFTS", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shiftTeams[(int)reader["SHIFT_ID"]] = (int)reader["TEAM"];
                    }
                }


                Random r = new Random(0);
                while (true)
                {
                    // Get all the possible users in the current shift
                    var teamNumberForCurrentShift = shiftTeams[(int)(SqlInt32)session.OutputRow["SHIFT_ID"]];
                    var usersInTeam = ShiftUsers[teamNumberForCurrentShift];

                    // Pick a user at random
                    var userId = usersInTeam[r.Next(usersInTeam.Count)];

                    yield return userId;
                   
                }
            }
            
        }
    }

}
