using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreatureController : MonoBehaviour
{

    List<Creatures.Creature> creatureList = new List<Creatures.Creature>();


    
    public Dictionary<Creatures.Creature,List<OverlayTile>> pathList = new Dictionary<Creatures.Creature, List<OverlayTile>>();

    public PathFinder pathFinder;

    float tickrate = 15;
    float timer;


    private void Start()
    {
       pathFinder = gameObject.GetComponentInChildren<PathFinder>();
        timer = 60 / 60 / tickrate;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // Refreshes list of creatures in controllers creatureList
            RefreshCreatureList(creatureList);

            // Checking for possible creature movement requests.
            CheckPathRequests(creatureList);
            
            timer = 60 / 60 / tickrate;
        }

        // Going through the pathlist containing
        // creature path handling and movement.
        ProcessPathlist();

    }

    /// <summary>
    /// Makes creature move on it's path given by the pathfinding algorithm.
    /// </summary>
    /// <param name="character"></param>
    /// <param name="path"></param>
    private void MoveAlongPath(Creatures.Creature character, List<OverlayTile> path)
    {
        var step = character.NormalSpeed * Time.deltaTime;
        float zIndex;
        // Starts moving towards next tile (first element in the path list)
        
        if(path.Count == 0)
        {
            return;
        }

        zIndex = path[0].transform.position.z;
        
        
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
        character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);

        // When the creature is close enough to the new tile, position it precisely on new tile and remove current tile from path.
        if (Vector2.Distance(character.transform.position, path[0].transform.position) < 0.0001f)
        {
            PositionCharacterOnTile(character, path[0]);
            
            path.RemoveAt(0);
        }
    }

    /// <summary>
    /// Places the creature on precise point over the tile. Also changes the creature's active tile. 
    /// </summary>
    /// <param name="character"></param>
    /// <param name="tile"></param>
    public void PositionCharacterOnTile(Creatures.Creature character, OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;

        // As the creature is on the tile, creature's properties are be updated!
        character.ActiveTile.creaturesOnTile.Remove(character);
        character.PreviousTile = character.ActiveTile;
        character.ActiveTile = tile;
        character.ActiveTile.creaturesOnTile.Add(character);

    }

    /// <summary>
    /// Will generate and set new paths for creatures that have pathRequest as true.
    /// </summary>
    /// <param name="creature"></param>
    private void CheckPathRequests(List<Creatures.Creature> creatureList)
    {
        // How many path requests per tick can the script process.
        int requestCap = 10;
        int requests = 0;

        // Checking creature path request status
        foreach (Creatures.Creature creature in creatureList)
        {
            if (creature.PathRequest)
            {
                List<string> movementTypes = creature.MovementTypeTileNames;

                // New list for future path
                List<OverlayTile> path = new List<OverlayTile>();

                // If there's already existing path, remove it.
                // Previous path will be replaced with a new one later on. 
                if (pathList.ContainsKey(creature))
                {
                    pathList.Remove(creature);
                }

                //creature.TargetTile != creature.ActiveTile &&
                // If the desired location differs from the current one, initiate pathfinding process:
                if ((requests < requestCap))
                {
                    if(creature.ActiveTile != creature.TargetTile)
                    {
                        path = pathFinder.FindPath(creature.ActiveTile, creature.TargetTile, movementTypes);
                        pathList.Add(creature, path);

                        if (!creature.Moving)
                        {
                            try
                            {
                                creature.ActiveTile.creaturesOnTile.Remove(creature);
                            } catch { }
                            
                        }
                    }




                    creature.ActiveTile.occupied = false;
                    creature.Moving = true;

                    requests++;
                    creature.PathRequest = false;
                }

                

                if (creature.ReservedTile != null)
                {
                    creature.ReservedTile.reserved = false;
                    creature.ReservedTile = null;
                }
            }
            else
            {
                // Prevents spamclick abuse to freeze goblin between tiles
                if(creature.Moving && !pathList.ContainsKey(creature))
                {
                    var newEndTile = pathFinder.FindClosestPassable(creature.ActiveTile, creature.ActiveTile, creature.MovementTypeTileNames);
                    pathList[creature] = pathFinder.FindPath(creature.ActiveTile, newEndTile, creature.MovementTypeTileNames);
                    creature.ActiveTile.creaturesOnTile.Remove(creature);
                }
            }
        }
    }

    /// <summary>
    /// Refreshes list of creatures in controllers creatureList
    /// </summary>
    /// <param name="creatureList"></param>
    private void RefreshCreatureList(List<Creatures.Creature> creatureList)
    {
        // Get all creatures and new creatures to the creatureList
        var objectArray = FindObjectsOfType<Creatures.Creature>();
        foreach (var i in objectArray)
        {
            // If the list doesn't contain observed creature, add it to the list.
            if (!creatureList.Contains(i))
            {
                creatureList.Add(i);
            }
        }
    }

    /// <summary>
    /// Goes through the pathList, handles creature movement.
    /// </summary>
    private void ProcessPathlist()
    {
        // Makes the character to move along desired path and chooses a new path if destination becomes blocked.   
        foreach (Creatures.Creature creature in pathList.Keys.ToList())
        {
            // Setting the current creature's path to the path variable
            List<OverlayTile> path = pathList[creature];

            // If path is greater than 0, it means the creature isn't at its desired destination yet.
            if (path.Count > 0)
            {
                var endTile = path[path.Count - 1];

                // If end tile is reserved to a current creature, no need to check target tile.
                if (endTile.reserved && endTile.reservedTo == creature && !creature.TimerHit)
                {

                }
                else
                {
                    NearTargetProcedure(creature, path, endTile);
                }
 
                MoveAlongPath(creature, path);

            }
            // When path list doesn't contain anything, the creature has arrived at the target destination
            else
            {

                // Checking if creature can still stay on end tile
                if (creature.ActiveTile.creaturesOnTile.Count() > 0 && creature.ActiveTile.creaturesOnTile[0] != creature)
                {
                    foreach (var c in creature.ActiveTile.creaturesOnTile)
                    {
                        // If creature is not moving, it means they are SET ON the tile. Not crossing through it
                        if(!c.Moving)
                        {
                            var newEndTile = pathFinder.FindClosestPassable(creature.ActiveTile, creature.ActiveTile, creature.MovementTypeTileNames);
                            path = pathFinder.FindPath(creature.ActiveTile, newEndTile, creature.MovementTypeTileNames);

                            // Overriding current path information
                            pathList[creature] = path;
                            continue;
                        }
                    }

                }
                pathList.Remove(creature);
                PositionCharacterOnTile(creature, creature.ActiveTile);
                
                // Update creature properties.
                creature.ActiveTile.occupied = true;
                creature.Moving = false;
                if (creature.ReservedTile != null)
                {
                    creature.ReservedTile.reserved = false;
                    creature.ReservedTile = null;
                }
            }
        }

    }

    /// <summary>
    /// Prevents creatures from walking on same tile and also optimizes mass movement.
    /// </summary>
    /// <param name="creature"></param>
    /// <param name="path"></param>
    /// <param name="endTile"></param>
    private void NearTargetProcedure(Creatures.Creature creature, List<OverlayTile> path, OverlayTile endTile)
    {
        List<string> movementTypes = creature.MovementTypeTileNames;

        // When the creature comes closer to the target destination, check if tile is still allowed as end tile.
        // This is to prevent several creatures stopping on same tile.
        if (path.Count < 7 && !pathFinder.CheckIfPassable(endTile, movementTypes))
        {

            int i = 0;
            bool x = true;
            while (x || i < 10)
            {
                endTile = path[path.Count - 1];

                // If end tile has become occupied or reserved by another creature, find a path to the closest unoccupied tile:
                if (endTile.occupied)
                {
                    var passable = pathFinder.FindClosestPassable(creature.ActiveTile, creature.TargetTile, movementTypes);
                    path = pathFinder.FindPath(creature.ActiveTile, passable, movementTypes);

                    // When assigning new path, it is possible that the character will move back to previous tile. 
                    // This handles that occurence.
                    if (path.Count == 0)
                    {
                        path.Add(creature.ActiveTile);
                        endTile.reservedTo = creature;
                        creature.ReservedTile = endTile;
                    }
                }
                else
                {
                    endTile.reservedTo = creature;
                    creature.ReservedTile = endTile;
                    endTile.reserved = true;
                    
                    x = false;
                }

                i++;
            }

            pathList[creature] = path;
        }
    }

}
