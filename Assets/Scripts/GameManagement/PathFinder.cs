using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using System;

public class PathFinder : MonoBehaviour
{

    Dictionary<OverlayTile, float> CLOSEST_PASSABLE_processedTiles = new Dictionary<OverlayTile, float>();
    float TILE_LIFETIME = 30;
    int BOUNDS_SIZE = 0;
    BoundsInt BOUNDS;

    float MAP_REFRESH_TIMER_VALUE = 5f;
    float MAP_REFRESH_TIMER = 0f;
    Dictionary<Vector2Int, OverlayTile> MAP;
    private void Start()
    {
        var x = GameObject.Find("Grid").GetComponent<MapManager>().GetComponentInChildren<Tilemap>().cellBounds;
        BOUNDS = x;
        BOUNDS_SIZE = Math.Abs(x.yMin) + Math.Abs(x.yMax) * Math.Abs(x.xMax) + Math.Abs(x.xMin) * Math.Abs(x.zMax) + Math.Abs(x.zMin);
        MAP = MapManager.Instance.map;
    }

    private void FixedUpdate()
    {
        // Reduces lifetime stored into dictionary and removed the item after lifetime end.
        foreach(var key in CLOSEST_PASSABLE_processedTiles.Keys)
        {
            CLOSEST_PASSABLE_processedTiles[key] -= Time.deltaTime;

            if(CLOSEST_PASSABLE_processedTiles[(key)] < 0)
                CLOSEST_PASSABLE_processedTiles.Remove(key);
        }

        MAP_REFRESH_TIMER -= Time.deltaTime;

        // Reload map after timer runs out
        if(MAP_REFRESH_TIMER < 0)
        {
            MAP_REFRESH_TIMER = MAP_REFRESH_TIMER_VALUE;
            MAP = MapManager.Instance.map;
        }

    }

    /// <summary>
    /// Finds path between two tiles.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="characterData"></param>
    /// <returns></returns>
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end, List<string> movementTypes)
    {
        if(start == null)
        {
            Debug.Log("Given start tile is null");
            return null;
        }
        // openList contains tiles that have not been processed yet.
        //List<OverlayTile> openList = new List<OverlayTile>();

        Heap<OverlayTile> openList = new Heap<OverlayTile>(BOUNDS_SIZE);

        // closedList contains processed tiles.
        // It will be used to skip already processed tiles.
        HashSet<OverlayTile> closedList = new HashSet<OverlayTile>();

        openList.Add(start);

        // If the tile is not passable, find a closest passable tile. Set it as end tile. 
        if (!CheckIfPassable(end, movementTypes))
        {
            end = FindClosestPassable(start, end, movementTypes);
        }

        int maxIndex = 0;
        while (openList.Count > 0 && maxIndex < 10000)
        {
            // Set tile with the shortest distance between start and end point as the current tile.
            OverlayTile currentOverlayTile = openList.RemoveFirst();

            // As the code is now processing the tile from openList, remove it from openList and add into closedList.
            // (closedList will contain only tiles of the desired path)
            //openList.RemoveFirst();
            closedList.Add(currentOverlayTile);

            if(currentOverlayTile == end)
            {
                return GetFinishedList(start, end);
            }

            var neighbourTiles = GetNeighbourTiles(currentOverlayTile);
            int length = neighbourTiles.Count;
            // Goes through every neighbouring tile and chooses one with which has the shortest path going to the target destination.
            for (int i = 0; i < length; i++)
            {
                var neighbour = neighbourTiles.First();
                neighbourTiles.Remove(neighbourTiles.First());

                // Skip tile if it contains of the following:
                if (!movementTypes.Contains(neighbour.tileType) || closedList.Contains(neighbour) || Mathf.Abs(currentOverlayTile.gridLocation.z - neighbour.gridLocation.z) > 1)
                {
                    continue;
                }

                // Gets distances from start and end points to the neighbour.
                // 
                neighbour.G = GetManhattanDistance(start, neighbour);
                neighbour.H = GetManhattanDistance(end, neighbour);

                // Set the previous tile value as current. So backtracking from end point is possible.
                neighbour.previous = currentOverlayTile;

                // One of the neighbouring tiles (with the optimal path)
                // will be processed on next iteration.
                if (!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
            maxIndex += 1;
        }

        return new List<OverlayTile>();
    }

    /// <summary>
    /// Returns the completed path between tiles.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishedList = new List<OverlayTile> ();

        OverlayTile currentTile = end;

        // Back tracking from end tile to the start with the help of
        // "previous" variable.
        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }

        finishedList.Reverse();

        return finishedList;
    }
    
    private double GetManhattanDistance(OverlayTile start, OverlayTile neighbour)
    {
        // Adds a bit of variation to the chosen path.
        double randMultiplier = 1;
        var x = UnityEngine.Random.Range(1, 120);
        if (x < 2)
            randMultiplier = UnityEngine.Random.Range(2f, 3f);

        //int dist = Convert.ToInt16(randMultiplier * (Math.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Math.Abs(start.gridLocation.y - neighbour.gridLocation.y)));
        double dist = randMultiplier * (Math.Sqrt(Math.Pow(start.gridLocation.x - neighbour.gridLocation.x,2) + Math.Pow(start.gridLocation.y - neighbour.gridLocation.y,2)));
        return dist;
    }

    /// <summary>
    /// Returns the neighbouring tiles in four directions. (left, right, top, bottom)
    /// </summary>
    /// <param name="currentOverlayTile"></param>
    /// <returns></returns>
    private HashSet<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile)
    {
        // Debug line, should be removed later on.
        //GameObject.Find("GameManager").GetComponent<PathCalculationCounter>().counter++;

        var map = MapManager.Instance.map;
        HashSet<OverlayTile> neighbours = new HashSet<OverlayTile>();

        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                // Skip the current tile itself
                if (xOffset == 0 && yOffset == 0)
                    continue;

                Vector2Int locationToCheck = new Vector2Int(
                    currentOverlayTile.gridLocation.x + xOffset,
                    currentOverlayTile.gridLocation.y + yOffset
                );

                if (locationToCheck.x < BOUNDS.xMin || locationToCheck.x > BOUNDS.xMax || locationToCheck.y < BOUNDS.yMin || locationToCheck.y > BOUNDS.yMax)
                    continue;

                // Maybe try catch isn't as costly as comparing dictionary.
                try
                {
                    neighbours.Add(MAP[locationToCheck]);
                }
                catch {
                    //Debug.Log("Map doesn't contain key");
                }
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Returns boolean if tile is passable on depending on movement types.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="movementTypes"></param>
    /// <returns></returns>
    public bool CheckIfPassable(OverlayTile tile, List<string> movementTypes)
    {
        if (!movementTypes.Contains(tile.tileType) || tile.occupied || tile.reserved || tile.creaturesOnTile.Count() > 0)
        {
            return false;
        }
        return true;
    }

    public OverlayTile FindClosestPassable(OverlayTile startTile,OverlayTile targetTile, List<string> movementTypes)
    {
        var neighbours = GetNeighbourTiles(targetTile);
        List<OverlayTile> processedTiles = new List<OverlayTile>();
        List<OverlayTile> notProcessedTiles = new List<OverlayTile>();


        List<OverlayTile> possibleCandidates = new List<OverlayTile>();



        OverlayTile temporaryTile = new OverlayTile();
        temporaryTile.gridLocation = new Vector3Int(-999, -999, -999);
        OverlayTile closestNeighbour = temporaryTile;

        int cap = 0;
        // Goes through neighbours, returns one, if passable. Else continues to search for further tiles.
        while(cap < 1000)
        {
            int setCount = neighbours.Count;
            for(int i = 0; i < setCount; i++)
            {
                var neighbour = neighbours.First();
                neighbours.Remove(neighbours.First());

                if (CheckIfPassable(neighbour, movementTypes))
                {
                    possibleCandidates.Add(neighbour);
                }

                if (!processedTiles.Contains(neighbour))
                {
                    notProcessedTiles.Add(neighbour);
                }
            }

            if (possibleCandidates.Count > 0)
            {
                // Go through each possible tile candidate
                foreach (var neighbour in possibleCandidates)
                {

                    // CLOSEST_PASSABLE_processedTiles.Add(neighbour, TILE_LIFETIME);
                    processedTiles.Add(neighbour);
                    if (closestNeighbour == temporaryTile)
                    {
                        closestNeighbour = neighbour;
                        continue;
                    }

                    else if (GetManhattanDistance(startTile, neighbour) < GetManhattanDistance(startTile, closestNeighbour))
                    {
                        closestNeighbour = neighbour;
                    }

                }
                return closestNeighbour;
            }
             
            // Set a new list of neighbouring tiles.
            neighbours = new HashSet<OverlayTile>();
            // Get neighbouring tiles of each needed tiles and add them into the neighbours list.
            foreach (OverlayTile tile in notProcessedTiles)
            {

                // Get new tiles to expand the search upon. 
                HashSet<OverlayTile> newTiles = GetNeighbourTiles(tile);
                setCount = newTiles.Count;
                // Add the new neighbour tiles to the neigbour list.
                for(int i = 0; i < setCount; i++)
                {
                    neighbours.Add(newTiles.First());
                    newTiles.Remove(newTiles.First());
                }
            }

            // Since the all current tiles in notProcessedTiles were processed,
            // empty it for the next iteration 
            notProcessedTiles = new List<OverlayTile>();

            if (neighbours.Count <= 0)
            {
                Debug.Log("No fitting passable tiles found.");
                return null;
            }

            cap++;
        }

        return null;

    }

    public void ChunkMatrix(int chunkSize, Vector2 offset)
    {

    }

    /// <summary>
    /// Checks if creature can traverse from chunk entry point to possible endpoint.
    /// Between two faces of the chunk with the given movement type.
    /// </summary>
    private void CheckChunkPassable(Vector2 chunkPos, int chunkSize, List<string> movementTypes, StructLibrary.ChunkTraverseOrientation movementDirection)
    {
        var bounds = GameObject.Find("Grid").GetComponent<MapManager>().GetComponentInChildren<Tilemap>().cellBounds;

        Vector2 offset = new Vector2(0 - bounds.x, 0 - bounds.y);


        var centerTileCoordinates = ((chunkPos.x * chunkSize + 1) - offset.x,
                                     (chunkPos.y * chunkSize + 1) - offset.y);

        //Dictionary<int, int> locationsToLightUp =
        //{
        //    ]
        //};



        // Check for blocked faces:
        


    }

    /// <summary>
    /// Returns all neighboring chunks.
    /// </summary>
    /// <param name="chunkPos"></param>
    private void GetNeighbourChunks(Vector2 chunkPos)
    {

    }



}
