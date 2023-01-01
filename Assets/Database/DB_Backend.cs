using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using Goblin;
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

        public bool SaveGoblin(Goblin.Goblin goblin)
        {
            bool success = false;
            int id = 99;
            if(goblin.GoblinId == 0)
            {
                string command1 = $"INSERT INTO goblins (first_name, last_name) VALUES ('{goblin.FirstName}', '{goblin.LastName}')";
                using (var connection = new SqliteConnection(_GoblinDB))
                {
                    connection.Open();
                    using (var sqlCommand = connection.CreateCommand())
                    {
                        sqlCommand.CommandText = "SELECT id from goblins ORDER BY id DESC limit 1";
                        SqliteDataAdapter adapter = new SqliteDataAdapter("SELECT id from goblins ORDER BY id DESC limit 1", connection);

                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow row in table.Rows)
                        {
                            goblin.GoblinId = int.Parse(row[0].ToString());
                            break;
                        }
                        connection.Close();
                    }
                }
            }
            if (goblin.GoblinId != 0)
            {

            }

            //string command = $""



            //using (var connection = new SqliteConnection(_GoblinDB))
            //{
            //    ExecuteCommand(connection, command);
            //}


            return success;
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
    }
}

