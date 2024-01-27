using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreatureController : MonoBehaviour
{

    List<Creatures.Creature> creatureList = new List<Creatures.Creature>();

    
    public Dictionary<Creatures.Creature,List<OverlayTile>> pathList = new Dictionary<Creatures.Creature, List<OverlayTile>>();

    private PathFinder pathFinder = new PathFinder();

    float tickrate;

    private void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {









        //FixCreaturePositioning(creatureList);
        RefreshCreatureList(creatureList);

        
        foreach (Creatures.Creature creature in creatureList)
        {
            CheckPathRequests(creature);
        }

        ProcessPathlist();


    }


    private void MoveAlongPath(Creatures.Creature character, List<OverlayTile> path)
    {
        var step = character.normalspeed * Time.deltaTime;

        var zIndex = path[0].transform.position.z;
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
        character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);

        if (Vector2.Distance(character.transform.position, path[0].transform.position) < 0.0001f)
        {
            PositionCharacterOnTile(character, path[0]);
            
            path.RemoveAt(0);
        }
    }


    public void PositionCharacterOnTile(Creatures.Creature character, OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;


        // Aktiiviseksi tileksi tulee uusi tile
        //character.activeTile.occupied = false;
        character.previousTile = character.activeTile;
        
        character.activeTile = tile;

        // Uusi tile tulee olemaan occupied
        //tile.occupied = true;
        
        
    }

    private void CheckPathRequests(Creatures.Creature creature)
    {

        if (creature.pathRequest)
        {
            List<string> a = new List<string> { "grass", "grass_slab" };
            List<OverlayTile> path = new List<OverlayTile>();
           
            if (pathList.ContainsKey(creature))
            {
                pathList.Remove(creature);
            }
            // If creature should begin moving, find path and make status changes for creature
            if (creature.targetTile != creature.activeTile)
            {
                path = pathFinder.FindPath(creature.activeTile, creature.targetTile, a);
                pathList.Add(creature, path);

                creature.activeTile.occupied = false;
                creature.moving = true;
            }

            creature.pathRequest = false;
            
        }
    }

    private void RefreshCreatureList(List<Creatures.Creature> creatureList)
    {
        // Get all creatures and new creatures to the creatureList
        var objectArray = FindObjectsOfType<Creatures.Creature>();
        foreach (var i in objectArray)
        {
            if (!creatureList.Contains(i))
            {
                creatureList.Add(i);
            }
        }
    }


    private void FixCreaturePositioning(List<Creatures.Creature> creatures)
    {
       // Dictionary<Creatures.Creature, OverlayTile> creatureTiles = new Dictionary<Creatures.Creature, OverlayTile>();
       // foreach (Creatures.Creature creature in creatures)
       // {
       //     if (creature.moving)
       //         continue;
       //     else
       //         creatureTiles.Add(creature, creature.activeTile);
       // }

       //// creatures.GroupBy<>
       // //var y = creatureTiles.Values.GroupBy(x => x)

       // foreach(var x in )
       // {

       //     foreach (var creature in creatureTiles.Keys)
       //     {

       //     }
       // }
    }

    private void ProcessPathlist()
    {
        // Makes the character to move along desired path and choosing a new path if destination becomes blocked.   
        foreach (Creatures.Creature creature in pathList.Keys)
        {

            List<OverlayTile> path = pathList[creature];
            if (path.Count > 0)
            {
                List<string> a = new List<string> { "grass", "grass_slab" };
                if (path.Count < 5 && !pathFinder.CheckIfPassable(path[path.Count - 1], a))
                {
                    //path = pathFinder.FindPath(creature.activeTile, creature.targetTile, a);
                    int i = 0;
                    bool x = true;
                    while (x || i < 10)
                    {

                        if (path[path.Count - 1].occupied)
                        {
                            var passable = pathFinder.FindClosestPassable(creature.activeTile, creature.targetTile, a);
                            path = pathFinder.FindPath(creature.activeTile, passable, a);

                            if (path.Count == 0)
                            {

                                path.Add(creature.activeTile);

                            }


                        }
                        else
                        {
                            x = false;
                        }

                        i++;
                    }

                    pathList[creature] = path;
                }
                MoveAlongPath(creature, path);

            }
            else
            {
                pathList.Remove(creature);
                Debug.Log("Removed creature from pathlist");
                PositionCharacterOnTile(creature, creature.activeTile);


                if (path.Count == 0)
                {
                    creature.activeTile.occupied = true;
                    creature.moving = false;
                }
            }
        }

    }

}
