using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database.Communicator;

namespace Goblin.Creator
{
    public class GoblinCreator : MonoBehaviour
    {
        Database.Communicator.DatabaseCommunicator dbCommunicator;

        // Start is called before the first frame update
        void Start()
        {
            
            Database.Communicator.DatabaseCommunicator dbCommunicator = new Database.Communicator.DatabaseCommunicator();
            dbCommunicator.InitializeDatabases();
            dbCommunicator.SaveGoblin(CreateDefaultGoblin());
        }

        /// <summary>
        /// Creates the 'usual' goblins. Mainly used at the start of the game.
        /// </summary>
        public Goblin CreateDefaultGoblin()
        {
            Goblin goblin = new Goblin();
            goblin.GoblinId = 0;
            goblin = GenerateEmotions(goblin);
            goblin = GenerateStats(goblin);
            goblin = GenerateName(goblin);

            return goblin;
        }

        private Goblin GenerateEmotions(Goblin goblin)
        {
            goblin.Sadness = 0;
            goblin.Happiness = 50;
            goblin.Fear = 0;
            goblin.Anger = 0;
            goblin.Surprise = 0;
            goblin.Disgust = 0;

            return goblin;
        }

        private Goblin GenerateStats(Goblin goblin)
        {
            goblin.Hp = 50;
            goblin.Strength = Random.Range(1, 10);
            goblin.Defense = Random.Range(1, 10);
            goblin.Tiredness = 0;
            goblin.Hunger = 0;
            goblin.Cleanliness = 0;
            goblin.Sanity = 100;
            goblin.Sickness = 0;

            goblin.Living = true;
            goblin.Working = false;
            goblin.DoingActivity = false;
            goblin.Hostile = false;

            int num = Random.Range(1, 100);
            if (num % 2 == 0)
            {
                goblin.Gender = "Male";
            }
            else
                goblin.Gender = "Female";


            return goblin;
        }

        private Goblin GenerateName(Goblin goblin)
        {
            goblin.FirstName = "Markku";
            goblin.LastName = "McGyver";
            return goblin;
        }

    }

}
