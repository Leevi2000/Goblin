using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{

    List<Creatures.Creature> creatureList = new List<Creatures.Creature>();

    Dictionary<Creatures.Creature,List<OverlayTile>> pathList = new Dictionary<Creatures.Creature, List<OverlayTile>>();

    private PathFinder pathFinder = new PathFinder();
   // private List<OverlayTile> path = new List<OverlayTile>();

    float tickrate;
   // private Creatures.Creature character;
    private void Start()
    {
       // pathFinder = new PathFinder();
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


                    path = pathFinder.FindPath(creature.activeTile, creature.targetTile, a);
                    creature.pathRequest = false;
                    
                    pathList.Add(creature, path);
                }

            }
        }

        foreach (Creatures.Creature creature in pathList.Keys)
        {
            //List<string> a = new List<string> { "grass", "grass_slab" };
            //List<OverlayTile> path = new List<OverlayTile>();
            //if (creature.pathRequest)
            //{
            //    path = pathFinder.FindPath(character.activeTile, character.targetTile, a);
            //    creature.pathRequest = false;
            //}
            List<OverlayTile> path = pathList[creature];
            if (path.Count > 0)
            {
                MoveAlongPath(creature, path);
               // CheckTarget();
            }
            else
            {
                pathList.Remove(creature);
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
            //character.activeTile = path[0];
            //path[0].occupied = true;
            
            path.RemoveAt(0);
        }
    }


    private void PositionCharacterOnTile(Creatures.Creature character, OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;

        // Ensin aiempi tile laitetaan unoccupied
        character.previousTile.occupied = false;

        // Aktiiviseksi tileksi tulee uusi tile
        character.previousTile = character.activeTile;
        character.activeTile = tile;

        // Uusi tile tulee olemaan occupied
        tile.occupied = true;
        
        
    }

}
