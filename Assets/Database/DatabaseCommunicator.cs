using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database.Backend;

namespace Database.Communicator
{
    public class DatabaseCommunicator : MonoBehaviour
    {
        DB_Backend goblinData = new DB_Backend();

        // Start is called before the first frame update
        void Start()
        {
            
        }


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


        // Update is called once per frame
        void Update()
        {

        }
    }
}

