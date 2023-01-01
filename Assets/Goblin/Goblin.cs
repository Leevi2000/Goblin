using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goblin
{
    public class Goblin 
    {
  


        [SerializeField]
        int goblinId;

        // Emotions
        int sadness;
        int happiness;
        int fear;
        int anger;
        int surprise;
        int disgust;

        // Other Stats
        int hp;
        int strength;
        int defense;
        int tiredness;
        int hunger;
        int cleanliness;
        int sanity;
        int sickness;

        
        bool living;
        string reasonOfDeath;
        string profession;
        bool working;
        bool doingActivity;
        bool hostile;

        int age;
        [SerializeField]
        string firstName;
        [SerializeField]
        string lastName;
        string gender;

        int durrentTaskId;



        public Goblin()
        {

        }

        public int GoblinId { get => goblinId; set => goblinId = value; }
        public int Sadness { get => sadness; set => sadness = value; }
        public int Happiness { get => happiness; set => happiness = value; }
        public int Fear { get => fear; set => fear = value; }
        public int Anger { get => anger; set => anger = value; }
        public int Surprise { get => surprise; set => surprise = value; }
        public int Disgust { get => disgust; set => disgust = value; }
        public int Hp { get => hp; set => hp = value; }
        public int Strength { get => strength; set => strength = value; }
        public int Defense { get => defense; set => defense = value; }
        public int Tiredness { get => tiredness; set => tiredness = value; }
        public int Hunger { get => hunger; set => hunger = value; }
        public int Cleanliness { get => cleanliness; set => cleanliness = value; }
        public int Sanity { get => sanity; set => sanity = value; }
        public int Sickness { get => sickness; set => sickness = value; }
        public bool Living { get => living; set => living = value; }
        public string ReasonOfDeath { get => reasonOfDeath; set => reasonOfDeath = value; }
        public string Profession { get => profession; set => profession = value; }
        public bool Working { get => working; set => working = value; }
        public bool DoingActivity { get => doingActivity; set => doingActivity = value; }
        public bool Hostile { get => hostile; set => hostile = value; }
        public int Age { get => age; set => age = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Gender { get => gender; set => gender = value; }
        public int DurrentTaskId { get => durrentTaskId; set => durrentTaskId = value; }
    }
}

