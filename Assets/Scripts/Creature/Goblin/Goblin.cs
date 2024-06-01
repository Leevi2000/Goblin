using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// Should be moved under dto namespace
namespace Creatures
{

    public class Goblin : Creature
    {
        int age;
        string gender;

        public float sadness = 0.4f;
        public float happiness = 0.4f;
        public float fear = 0.4f;
        public float anger = 0.4f;
        public float surprisement = 0.4f;
        public float disgust = 0.4f;

        public float tiredness = 0.4f;
        public float hunger = 0.4f;
        public float cleanliness = 0.4f;
        public float sanity = 0.4f;
        public float sickness = 0.4f;

        int taskId;

        Dictionary<string, float> proficiencies = new Dictionary<string, float>()
        {
            {"woodcutting", 0f },
            {"mining", 0f },
            {"foraging", 0f },
            {"building", 0f },
            {"combat", 0f }
        };


    }
}

