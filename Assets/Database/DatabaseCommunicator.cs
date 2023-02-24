using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database.Backend;

namespace Database.Communicator
{
    public class DatabaseCommunicator : MonoBehaviour
    {
        DB_Backend goblinData = new DB_Backend();


        public void SaveGoblin(Goblin.Goblin goblin)
        {
            goblinData.SaveGoblin(goblin);
        }

        public void InitializeDatabases()
        {
            goblinData.InitializeDatabases();
        }

        public Goblin.Goblin LoadGoblin(int goblinId)
        {
            return goblinData.LoadGoblin(goblinId);
        }


    }
}

