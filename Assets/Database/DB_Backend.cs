using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;


namespace Database.Backend
{
   public class DB_Backend : MonoBehaviour
    {
        // For storing the data of 'run-time' goblins.
        private string _GoblinDB = "URI=file:Goblins.db";

        private void Start()
        {
            DropGoblins();
            //InitializeDatabases();
        }


        public bool InitializeDatabases()
        {
            bool success = false;

            List<string> GoblinDBCreateTables = new List<string>();

            // Creates goblins table
            GoblinDBCreateTables.Add(
                "CREATE TABLE IF NOT EXISTS goblins (" +
                "id INTEGER NOT NULL PRIMARY KEY)");

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
                "first_name varchar(50), " +
                "last_name varchar(50), " +
                "gender varchar(10)" +
                "FOREIGN KEY(goblin_Id) REFERENCES goblins(id) " +
                "ON DELETE CASCADE ON UPDATE NO ACTION)");


            foreach (var item in GoblinDBCreateTables)
            {
                CreateDB(_GoblinDB, item);
            }

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

        private void DropGoblins()
        {

            File.Delete(_GoblinDB);
            using (var connection = new SqliteConnection(_GoblinDB))
            {
                connection.Open();
                ExecuteCommand(connection,
                    "select 'drop table ' || name || ';' from sqlite_master" +
                    "where type = 'table'");
                connection.Close();
            }
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

