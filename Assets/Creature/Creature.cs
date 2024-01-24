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
        List<string> movementTypes;
        List<string> inventory;

        public int normalspeed;
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

