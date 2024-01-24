using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;

namespace Database.Backend
{
   public class DB_Backend
    {
        // For storing the data of 'run-time' goblins.
        private string _GoblinDB = "URI=file:DatabaseData/Goblins.db";

     


        public bool InitializeDatabases()
        {
            bool success = false;

            List<string> GoblinDBCreateTables = new List<string>();

            // Creates goblins table
            GoblinDBCreateTables.Add(
                "CREATE TABLE IF NOT EXISTS goblins (" +
                "id INTEGER NOT NULL PRIMARY KEY, " +
                "first_name varchar(50), " +
                "last_name varchar(50))");

            // Creates relation table
            GoblinDBCreateTables.Add(
                "CREATE TABLE IF NOT EXISTS relations (" +
                "id INTEGER NOT NULL PRIMARY KEY, " +
                "goblin_Id INTEGER NOT NULL, " +
                "related_To_GoblinId INTEGER NOT NULL, " +
                "relation_Properties varchar(200), " +
                "friendly_Level INTEGER NOT NULL, " +
                "FOREIGN KEY(goblin_Id) REFERENCES goblins(id) " +
                "ON DELETE CASCADE ON UPDATE NO ACTION)");

            // Creates emotion table
            GoblinDBCreateTables.Add(
                "CREATE TABLE IF NOT EXISTS emotions (" +
                "id INTEGER NOT NULL PRIMARY KEY, " +
                "goblin_Id INTEGER NOT NULL, " +
                "sadness INTEGER NOT NULL, " +
                "happiness INTEGER NOT NULL, " +
                "fear INTEGER NOT NULL, " +
                "anger INTEGER NOT NULL, " +
                "surprise INTEGER NOT NULL, " +
                "disgust INTEGER NOT NULL, " +
                "FOREIGN KEY(goblin_Id) REFERENCES goblins(id) " +
                "ON DELETE CASCADE ON UPDATE NO ACTION)");

            // Creates tasks table
            GoblinDBCreateTables.Add(
                "CREATE TABLE IF NOT EXISTS tasks (" +
                "id INTEGER NOT NULL PRIMARY KEY, " +
                "goblin_id INTEGER NOT NULL, " +
                "task_priority INTEGER NOT NULL, " +
                "task_details varchar(200), " +
                "task_type varchar(200), " +
                "FOREIGN KEY(goblin_Id) REFERENCES goblins(id) " +
                "ON DELETE CASCADE ON UPDATE NO ACTION)");

            // Creates stats table
            GoblinDBCreateTables.Add(
                "CREATE TABLE IF NOT EXISTS stats (" +
                "id INTEGER NOT NULL PRIMARY KEY, " +
                "goblin_id INTEGER NOT NULL, " +
                "hp INTEGER NOT NULL, " +
                "strength INTEGER NOT NULL, " +
                "defense INTEGER NOT NULL, " +
                "tiredness INTEGER NOT NULL, " +
                "hunger INTEGER NOT NULL," +
                "cleanliness INTEGER NOT NULL," +
                "sanity INTEGER NOT NULL, " +
                "sickness INTEGER NOT NULL, " +
                "FOREIGN KEY(goblin_Id) REFERENCES goblins(id) " +
                "ON DELETE CASCADE ON UPDATE NO ACTION)");

            // Creates other details table
            GoblinDBCreateTables.Add(
                "CREATE TABLE IF NOT EXISTS details(" +
                "id INTEGER NOT NULL PRIMARY KEY, " +
                "goblin_id INTEGER NOT NULL, " +
                "living BOOL, " +
                "reason_of_death varchar(200), " +
                "profession varchar(200), " +
                "age INTEGER NOT NULL, " +
                "working BOOL, " +
                "doing_activity BOOL, " +
                "gender varchar(10), " +
                "FOREIGN KEY(goblin_Id) REFERENCES goblins(id) " +
                "ON DELETE CASCADE ON UPDATE NO ACTION)");


            foreach (var item in GoblinDBCreateTables)
            {
                CreateDB(_GoblinDB, item);
            }

            return success;
        }

        public bool SaveGoblin(DTO.Goblin goblin)
        {
    
            bool success = false;

            // If goblin has no id, it is then a new one. This part is used to get the 'new' id.
            if(goblin.GoblinId == 0)
            {
                using (var connection = new SqliteConnection(_GoblinDB))
                {
                    connection.Open();
                    using (var sqlCommand = connection.CreateCommand())
                    {
                        // Used to get the currently highest id, used to identify newly created goblin's id.
                        SqliteDataAdapter adapter = new SqliteDataAdapter("SELECT id from goblins ORDER BY id DESC limit 1", connection);

                        // Use a datatable to read data from sql request
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Get the first result at the first column (id)
                        foreach (DataRow row in table.Rows)
                        {
                            goblin.GoblinId = int.Parse(row[0].ToString()) + 1;
                            break;
                        }
                    }
                    connection.Close();
                }
            }


            // SQL statement for each 'table'
            string goblinTable = $"INSERT INTO goblins (first_name, last_name) VALUES ('{goblin.FirstName}', '{goblin.LastName}')";

            string detailsTable = $"INSERT INTO details (goblin_id, living, reason_of_death, profession, age, working, doing_activity, gender) " +
                $"VALUES ('{goblin.GoblinId}', '{goblin.Living}', '{goblin.ReasonOfDeath}', '{goblin.Profession}', '{goblin.Age}', '{goblin.Working}', '{goblin.DoingActivity}', '{goblin.Gender}')";

            string emotionTable = $"INSERT INTO emotions (goblin_id, sadness, happiness, fear, anger, surprise, disgust) " +
                $"VALUES ('{goblin.GoblinId}', '{goblin.Sadness}', '{goblin.Happiness}', '{goblin.Fear}', '{goblin.Anger}', '{goblin.Surprise}', '{goblin.Disgust}')";

            string statTable = $"INSERT INTO stats (goblin_id, hp, strength, defense, tiredness, hunger, cleanliness, sanity, sickness) " +
                $"VALUES ('{goblin.GoblinId}', '{goblin.Hp}', '{goblin.Strength}', '{goblin.Defense}', '{goblin.Tiredness}', '{goblin.Hunger}', '{goblin.Cleanliness}', '{goblin.Sanity}', '{goblin.Sickness}')";


            if (goblin.GoblinId != 0)
            {
                ExecuteCommand(goblinTable);
                ExecuteCommand(detailsTable);
                ExecuteCommand(emotionTable);
                ExecuteCommand(statTable);
            }

            //string command = $""



            //using (var connection = new SqliteConnection(_GoblinDB))
            //{
            //    ExecuteCommand(connection, command);
            //}


            return success;
        }

        public DTO.Goblin LoadGoblin(int goblinId)
        {
            DTO.Goblin goblin = new DTO.Goblin();


            string fetchCmd = $"SELECT * FROM goblins LEFT JOIN details ON goblins.id=details.goblin_id LEFT JOIN stats ON goblins.id=stats.goblin_id LEFT JOIN emotions ON goblins.id=emotions.goblin_Id WHERE goblins.id={goblinId}";


            using (var connection = new SqliteConnection(_GoblinDB))
            {
                connection.Open();
                using (var sqlCommand = connection.CreateCommand())
                {
                    // 
                    SqliteDataAdapter adapter = new SqliteDataAdapter(fetchCmd, connection);

                    // Use a datatable to read data from sql request
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    DataRow row = table.Rows[0];

                    goblin.FirstName = row["first_name"].ToString();
                    goblin.LastName = row["last_name"].ToString();
                    goblin.Living = bool.Parse(row["living"].ToString());
                    goblin.ReasonOfDeath = row["reason_of_death"].ToString();
                    goblin.Profession = row["profession"].ToString();
                    goblin.Age = int.Parse(row["age"].ToString());
                    goblin.Working = bool.Parse(row["working"].ToString());
                    goblin.DoingActivity = bool.Parse(row["doing_activity"].ToString());
                    goblin.Gender = row["gender"].ToString();
                    goblin.Hp = int.Parse(row["hp"].ToString());
                    goblin.Strength = int.Parse(row["strength"].ToString());
                    goblin.Defense = int.Parse(row["defense"].ToString());
                    goblin.Tiredness = int.Parse(row["tiredness"].ToString());
                    goblin.Hunger = int.Parse(row["hunger"].ToString());
                    goblin.Cleanliness = int.Parse(row["cleanliness"].ToString());
                    goblin.Sanity = int.Parse(row["sanity"].ToString());
                    goblin.Sickness = int.Parse(row["sickness"].ToString());
                    goblin.Sadness = int.Parse(row["sadness"].ToString());
                    goblin.Happiness = int.Parse(row["happiness"].ToString());
                    goblin.Fear = int.Parse(row["fear"].ToString());
                    goblin.Anger = int.Parse(row["anger"].ToString());
                    goblin.Surprise = int.Parse(row["surprise"].ToString());
                    goblin.Disgust = int.Parse(row["disgust"].ToString());
                   

                }
                
            }
            return goblin;
        }


        public bool CreateDB(string DBName, string commandString)
        {
            
            using(var connection = new SqliteConnection(DBName))
            {
               // string commandString = "";

                connection.Open();
                ExecuteCommand(connection, commandString);
                connection.Close();
            }

            return false;
        }

        /// <summary>
        /// Destroys the entire goblin database.
        /// </summary>
        private void DropGoblins()
        {
            string path = Application.dataPath.Replace("/Assets", "/DatabaseData") + "/Goblins.db";
            if (File.Exists(path)) { File.Delete(path); }
        }

        private bool ExecuteCommand(SqliteConnection connection, string command)
        {
            bool success = false;
            using (var sqlCommand = connection.CreateCommand())
            {
                sqlCommand.CommandText = command;
                sqlCommand.ExecuteNonQuery();
                success = true;
            }

            return success;
        }

        private bool ExecuteCommand(string command)
        {
            bool success = false;

            using (var connection = new SqliteConnection(_GoblinDB))
            {
                connection.Open();
                using (var sqlCommand = connection.CreateCommand())
                {
                    sqlCommand.CommandText = command;
                    sqlCommand.ExecuteNonQuery();
                    success = true;
                }
                connection.Close();
            }
            return success;
        }
    }
}

