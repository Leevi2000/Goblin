using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Creatures;

namespace Helper
{
    /// <summary>
    /// Extends on Logic script to make code more readable. Contains boolean helper functions based on goblin statistics.
    /// </summary>
    public static class LogicHelper
    {
        //Thresholds
        private const float TIREDNESS_THRESHOLD = 0.75f;
        private const float SICKNESS_THRESHOLD = 0.60f;
        private const float HUNGER_THRESHOLD = 0.5f;
        private const float ANGER_THRESHOLD = 0.8f;
        private const float FEAR_THRESHOLD = 0.75f;
        private const float SADNESS_THRESHOLD = 0.8f;
        private const float DISGUST_THRESHOLD = 0.75f;
        private const float HAPPINESS_THRESHOLD = 0.2f;

        public static bool HostileNearby(Goblin goblin)
        {
            return false;
        }

        public static bool TirednessThresholdSurpassed(Goblin goblin)
        {
            if (goblin.tiredness > TIREDNESS_THRESHOLD)
            {
                return true;
            }

            return false;
        }

        public static bool SicknessThresholdSurpassed(Goblin goblin)
        {
            if(goblin.sickness > SICKNESS_THRESHOLD)
            {
                return true;
            }

            return false;
        }

        public static bool HungerThresholdSurpassed(Goblin goblin)
        {
            if (goblin.hunger > HUNGER_THRESHOLD)
            {
                return true;
            }

            return false;
        }

        public static bool AngerThresholdSurpassed(Goblin goblin)
        {
            if (goblin.anger > ANGER_THRESHOLD)
            {
                return true;
            }

            return false;
        }

        public static bool FearThresholdSurpassed(Goblin goblin)
        {
            if (goblin.fear > FEAR_THRESHOLD)
            {
                return true;
            }

            return false;
        }

        public static bool DisgustThresholdSurpassed(Goblin goblin)
        {
            if (goblin.disgust > DISGUST_THRESHOLD)
            {
                return true;
            }

            return false;
        }

        public static bool SadnessThresholdSurpassed(Goblin goblin)
        {
            if (goblin.sadness > SADNESS_THRESHOLD)
            {
                return true;
            }

            return false;
        }

        public static bool HappinessThresholdSurpassed(Goblin goblin)
        {
            if (goblin.sadness < HAPPINESS_THRESHOLD)
            {
                return true;
            }

            return false;
        }
    }
}

