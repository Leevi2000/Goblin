using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// Should be moved under dto namespace
namespace Creatures
{

    public class Goblin : Creature
    {
        int age = 0;
        string gender = "null";

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
        public bool working = false;
        public Job job = new Job();

        [SerializeField] Dictionary<string, float> proficiencies = new Dictionary<string, float>();

        public int Age { get => age; set => age = value; }
        public string Gender { get => gender; set => gender = value; }

        private void Start()
        {
            InitializeProficiencies();
        }

        /// <summary>
        /// Initializes proficiency tree with zero proficiency in everything.
        /// </summary>
        void InitializeProficiencies()
        {
            foreach (var jobName in Jobs.jobList)
            {
                proficiencies[jobName] = 0f;
            }
            job._workId = 0;

            Debug.Log(job.ToString());

        }
    }

}

