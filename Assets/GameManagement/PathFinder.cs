using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PathFinder
{
    /// <summary>
    /// Finds path between two tiles.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="characterData"></param>
    /// <returns></returns>
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end, List<string> movementTypes)
    {
        // If the tile is not passable, find a closest passable tile. Set it as end tile. 
        if(!CheckIfPassable(end, movementTypes))
        {
            end = FindClosestPassable(end, movementTypes);
        }




        // openList contains tiles that have not been processed yet.
        List<OverlayTile> openList = new List<OverlayTile>();

        // closedList contains processed tiles.
        // It will be used to skip already processed tiles.
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            // Set tile with the shortest distance between start and end point as the current tile.
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            // As the code is now processing the tile from openList, remove it from openList and add into closedList.
            // (closedList will contain only tiles of the desired path)
            openList.Remove(currentOverlayTile);
            closedList.Add(currentOverlayTile);

            if(currentOverlayTile == end)
            {
                return GetFinishedList(start, end);
            }

            var neighbourTiles = GetNeighbourTiles(currentOverlayTile);

            // Goes through every neighbouring tile and chooses one with which has the shortest path going to the target destination.
            foreach (var neighbour in neighbourTiles)
            {
                // Skip tile if it contains of the following:
                if(!movementTypes.Contains(neighbour.tileType)  || closedList.Contains(neighbour) || Mathf.Abs(currentOverlayTile.gridLocation.z - neighbour.gridLocation.z) > 1 || neighbour.occupied)
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
                if(!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }

            }
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
    
    private int GetManhattanDistance(OverlayTile start, OverlayTile neighbour)
    {
        // Adds a bit of variation to the chosen path.
        float randMultiplier = 1;
        var x = UnityEngine.Random.Range(1, 100);
        if (x < 10)
            randMultiplier = UnityEngine.Random.Range(0.2f, 5f);

        int dist = Convert.ToInt16(randMultiplier * (Math.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Math.Abs(start.gridLocation.y - neighbour.gridLocation.y)));
        return dist;
    }

    /// <summary>
    /// Returns the neighbouring tiles in four directions. (left, right, top, bottom)
    /// </summary>
    /// <param name="currentOverlayTile"></param>
    /// <returns></returns>
    private List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile)
    {
        // Get information of all tiles 
        var map = MapManager.Instance.map;

        List<OverlayTile> neighbours = new List<OverlayTile>();

        //top
        Vector2Int locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x, 
            currentOverlayTile.gridLocation.y + 1
            );

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //bottom
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y - 1
            );

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }


        //right
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y
            );

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }


        //left
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y
            );

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        return neighbours;
    }

    /// <summary>
    /// Returns boolean if tile is passable on depending on movement types.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="movementTypes"></param>
    /// <returns></returns>
    private bool CheckIfPassable(OverlayTile tile, List<string> movementTypes)
    {
        if (!movementTypes.Contains(tile.tileType) || tile.occupied)
        {
            return false;
        }
        return true;
    }

    private OverlayTile FindClosestPassable(OverlayTile targetTile, List<string> movementTypes)
    {
        var neighbours = GetNeighbourTiles(targetTile);
        List<OverlayTile> processedTiles = new List<OverlayTile>();
        List<OverlayTile> notProcessedTiles = new List<OverlayTile>();
        // Goes through neighbours, returns one, if passable. Else continues to search for further tiles.
        while(true)
        {
            foreach (var neighbour in neighbours)
            {
                if (CheckIfPassable(neighbour, movementTypes))
                {
                    return neighbour;
                }

                
                if (!processedTiles.Contains(neighbour))
                {
                    notProcessedTiles.Add(neighbour);
                }
                
            }

            // Set a new list of neighbouring tiles.
            neighbours = new List<OverlayTile>();
            // Get neighbouring tiles of each needed tiles and add them into the neighbours list.
            foreach (OverlayTile tile in notProcessedTiles)
            {
                processedTiles.Add(tile);

                // Get new tiles to expand the search upon. 
                List<OverlayTile> newTiles = GetNeighbourTiles(tile);

                // Add the new neighbour tiles to the neigbour list.
                foreach(OverlayTile newTile in newTiles)
                {
                    neighbours.Add(newTile);
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
        }

    }



}
