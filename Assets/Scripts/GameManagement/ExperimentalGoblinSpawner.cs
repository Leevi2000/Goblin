using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalGoblinSpawner : MonoBehaviour
{
    public CreatureController creatureController;
    OverlayTile[] tiles;
    bool tilesGenerated = false;

    public GameObject goblinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!tilesGenerated)
        {
            tiles = GameObject.Find("OverlayContainer").GetComponentsInChildren<OverlayTile>();
            if(tiles.Length > 0)
            {
                //for (int i = 0; i < 15; i++)
                //{
                //    SummonGoblin();
                //}
                tilesGenerated = true;
            }
                

        }


    }

    public void SummonGoblin()
    {
        for (int i = 0; i < 10; i++)
        {
            int possibleTiles = 0;

            foreach (OverlayTile tile in tiles)
            {
                if (tile.tileType == "grass")
                {
                    possibleTiles++;
                }
            }

            var x = UnityEngine.Random.Range(1, possibleTiles - 1);


            var goblin = Instantiate(goblinPrefab);

            goblin.GetComponent<Creatures.Creature>().ActiveTile = tiles[x];
            //goblinPrefab.GetComponent<Creatures.Creature>().PreviousTile = tiles[x];
            goblin.GetComponent<Creatures.Creature>().TargetTile = tiles[x];

            creatureController.PositionCharacterOnTile(goblin.GetComponent<Creatures.Creature>(), tiles[x]);

        }

    }

    public void DeleteAllCreatures()
    {
        var creatures = GameObject.FindGameObjectsWithTag("Creature");
        foreach (var creature in creatures)
        {
            Destroy(creature);
        }    
    }
}
