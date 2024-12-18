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
        [SerializeField] public bool lightUpActiveTile_DBG = false;

        [SerializeField] private bool pathRequest;

        [SerializeField] private bool moving;
        
        private List<string> movementTypes = new List<string> { "land" };

        /// <summary>
        /// List of tilenames creature can walk on
        /// </summary>
        private List<string> movementTypeTileNames = new List<string>();

        private TileNames tileNames = new TileNames();

        // -------------------------------------

        // Variables that are used as creature's properties:
        string firstname;
        string lastname;

        [SerializeField] private float normalSpeed;
        [SerializeField] float attackRadius;
        [SerializeField] int hp;
        [SerializeField] private float defense;
        [SerializeField] private float strength;
        [SerializeField] private int wanderDistance;

        List<string> _inventory;

        bool hostile;
        bool living;

        // Used for pathfinding and decisionmaking
        float timerInitialValue = 0.05f;
        float timer;
        // on timerHit, all other scripts can do desired event
        bool timerHit = false;

        void CheckActions()
        {

        }

        public void MoveTo(OverlayTile tile)
        {
            targetTile = tile;
            pathRequest = true;
        }

        private void Awake()
        {
            movementTypeTileNames = tileNames.ReturnTilenamesByMovementType(movementTypes);
            timer = timerInitialValue;
        }

        private void Update()
        {
            // Fixes an error on instantiation.
            if (previousTile == null)
            {
                previousTile = activeTile;
            }

            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timerHit = true;
                timer = timerInitialValue;
            }
            else
            {
                timerHit = false;
            }

            if(lightUpActiveTile_DBG)
            {
                activeTile.SetColor(Color.blue);
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
        public bool TimerHit { get => timerHit; set => timerHit = value; }
        public int WanderDistance { get => wanderDistance; set => wanderDistance = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
    }
}

