using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creatures
{
    public class Creature : MonoBehaviour
    {

        public OverlayTile activeTile;
        public OverlayTile targetTile;
        public OverlayTile previousTile;
        public bool pathRequest;
        public bool moving;
        List<string> movementTypes;
        List<string> inventory;

        public float normalspeed;
        int hp;
        float attackRadius;

        void CheckActions()
        {

        }

        void MoveTo(OverlayTile tile)
        {

        }

        private void Update()
        {
            if (previousTile == null)
            {
                previousTile = activeTile;
            }
        }

    }
}

