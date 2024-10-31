using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper.TilemapOperations;

public class JobFinder : MonoBehaviour
{
    float timer = 0;

    Dictionary<Vector2Int, OverlayTile> MAP;
    // Reference to each tile in certain job
    // All trees under woodcutting for example
    [SerializeField] List<OverlayTile> woodcutterJobs;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (MAP == null)
        {
            MAP = MapManager.Instance.map;
            return;
        }

        if (timer < 0)
        {
            UpdateJobs();
            timer = 5;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void UpdateJobs()
    {
        woodcutterJobs.Clear();

        // Go through each tile and add them to jobs if those tho lists that have potential work to do.
        foreach (OverlayTile tile in MAP.Values)
        {
            // Add tile to woodcutter jobs if tiletype is tree
            foreach(string tilename in TileNames.trees)
            {
                

                if (tile.tileType.Contains(tilename))
                    woodcutterJobs.Add(tile);
                break;
            }

        }
             //   if(tile.tileType in trees[])
    }

    public OverlayTile ReturnWork(Creatures.Goblin goblin)
    {
        switch(goblin.job.GetJobName())
        {
            case "woodcutting":
                return ReturnClosest(woodcutterJobs, goblin.ActiveTile);
                
        }
        

        return null;
    }

    // Returns closest job 
    OverlayTile ReturnClosest(List<OverlayTile> tiles, OverlayTile currentTile)
    {
        if(tiles.Count == 0)
        {
            Debug.Log("No available jobs");
            return null;
        }

        OverlayTile closestTile = tiles[0];
        double closestDistance = 999;

        foreach(OverlayTile tile in tiles)
        {
            double newDistance = TilemapHelper.GetManhattanDistance(currentTile, tile);

            if(newDistance < closestDistance)
            {
                closestTile = tile;
                closestDistance = newDistance;
            }
        }
        
        return closestTile;
    }

}
