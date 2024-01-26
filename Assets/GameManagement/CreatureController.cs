using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        var objectArray = FindObjectsOfType<Creatures.Creature>();
        foreach (var i in objectArray)
        {
            if (!creatureList.Contains(i))
            {
                creatureList.Add(i);
            }
        }

        
        foreach (Creatures.Creature creature in creatureList)
        {
            if (creature.pathRequest)
            {
                List<string> a = new List<string> { "grass", "grass_slab" };
                List<OverlayTile> path = new List<OverlayTile>();
                if (creature.pathRequest)
                {
                    if (pathList.ContainsKey(creature))
                    {
                        pathList.Remove(creature);
                    }

                    if (creature.targetTile != creature.activeTile)
                    {
                        path = pathFinder.FindPath(creature.activeTile, creature.targetTile, a);
                        pathList.Add(creature, path);

                        creature.activeTile.occupied = false;
                    }
                    
                    creature.pathRequest = false;
                    
                    
                }
            }
        }

        foreach (Creatures.Creature creature in pathList.Keys)
        {

            List<OverlayTile> path = pathList[creature];
            if (path.Count > 0)
            {
                List<string> a = new List<string> { "grass", "grass_slab" };
                if (path.Count < 3 && !pathFinder.CheckIfPassable(path[path.Count - 1], a))
                {
                    path = pathFinder.FindPath(creature.activeTile, creature.targetTile, a);

                    pathList[creature] = path;
                }
                MoveAlongPath(creature, path);

                // Test this
               

            }
            else
            {
                pathList.Remove(creature);

                if (path.Count == 0)
                {
                    creature.activeTile.occupied = true;
                }
            }
        }

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

}
