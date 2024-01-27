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
        public OverlayTile reservedTile;
        

        public bool pathRequest;
        public bool moving;

        
        List<string> movementTypes = new List<string> { "land" };
        public List<string> movementTypeTileNames = new List<string>();

        List<string> inventory;

        public float normalspeed;
        int hp;
        float attackRadius;

        TileNames tileNames = new TileNames();

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

