using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creatures
{
    public class Creature : MonoBehaviour
    {
        // Variables that are used in pathfinding and pathfinding optimization:

        public OverlayTile activeTile;
        public OverlayTile targetTile;
        public OverlayTile previousTile;
        public OverlayTile reservedTile;

        public bool pathRequest;
        public bool moving;

        List<string> movementTypes = new List<string> { "land" };
        public List<string> movementTypeTileNames = new List<string>();
        TileNames tileNames = new TileNames();

        // -------------------------------------

        // Variables that are used as creature's properties:

        public float normalspeed;
        float attackRadius;
        int hp;

        List<string> inventory;

        void CheckActions()
        {

        }

        void MoveTo(OverlayTile tile)
        {

        }

        private void Start()
        {
            movementTypeTileNames = tileNames.ReturnTilenamesByMovementType(movementTypes);
        }

        private void Update()
        {
            // Fixes an error on instantiation.
            if (previousTile == null)
            {
                previousTile = activeTile;
            }
        }

    }
}

