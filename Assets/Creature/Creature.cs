using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creatures
{
    public class Creature : MonoBehaviour
    {
        // Variables that are used in pathfinding and pathfinding optimization:

        [SerializeField] private OverlayTile activeTile;
        [SerializeField] private OverlayTile targetTile;
        [SerializeField] private OverlayTile previousTile;
        [SerializeField] private OverlayTile reservedTile;

        [SerializeField] private bool pathRequest;

        [SerializeField] private bool moving;

        private List<string> movementTypes = new List<string> { "land" };
        private List<string> movementTypeTileNames = new List<string>();
        private TileNames tileNames = new TileNames();

        // -------------------------------------

        // Variables that are used as creature's properties:

        [SerializeField]  private float normalSpeed;
        [SerializeField]  float attackRadius;
        [SerializeField]  int hp;

        List<string> _inventory;

        

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



        public OverlayTile ActiveTile { get => activeTile; set => activeTile = value; }
        public OverlayTile TargetTile { get => targetTile; set => targetTile = value; }
        public OverlayTile PreviousTile { get => previousTile; set => previousTile = value; }
        public OverlayTile ReservedTile { get => reservedTile; set => reservedTile = value; }
        public bool PathRequest { get => pathRequest; set => pathRequest = value; }
        public bool Moving { get => moving; set => moving = value; }
        public List<string> MovementTypes { get => movementTypes; set => movementTypes = value; }
        public List<string> MovementTypeTileNames { get => movementTypeTileNames; set => movementTypeTileNames = value; }
        public TileNames TileNames { get => tileNames; set => tileNames = value; }
        public float NormalSpeed { get => normalSpeed; set => normalSpeed = value; }
    }
}

