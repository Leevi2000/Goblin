using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper.TilemapOperations
{
    public static class TilemapHelper
    {
        public static List<OverlayTile> GetTilesAroundTarget(OverlayTile overlayTile, int radius)
        {
            var bounds = GameObject.Find("PathFinder").GetComponent<PathFinder>().BOUNDS;
            Dictionary<Vector2Int, OverlayTile> MAP = MapManager.Instance.map;
            List<OverlayTile> tiles = new List<OverlayTile>();

            var tilecoordinates = GenerateSquare(overlayTile.gridLocation.x, overlayTile.gridLocation.y, radius);

            foreach (var coord in tilecoordinates)
            {   
                    var pos = new Vector2Int(coord.x, coord.y);

                    // Exclude all positions that are beyond map boundaries.
                    if (pos.x < bounds.xMin || pos.x > bounds.xMax || pos.y < bounds.yMin || pos.y > bounds.yMax || !MAP.ContainsKey(pos))
                        continue;

                    var tile = MAP[pos];
                    tiles.Add(tile);
            }

            return tiles;
        }

        public static List<(int x, int y)> GenerateSquare(int centerX, int centerY, int size)
        {
            List<(int x, int y)> coordinates = new List<(int x, int y)>();

            int radius = (int)Mathf.Round(size / 2);

            int minX = centerX - radius;
            int maxX = centerX + radius;
            int minY = centerY - radius;
            int maxY = centerY + radius;

            if (size % 2 == 0)
            {
                minX = centerX - radius + 1;
                maxX = centerX + radius;
                minY = centerY - radius + 1;
                maxY = centerY + radius;
            }
            else
            {
                // Calculate the bounds of the square
                minX = centerX - radius;
                maxX = centerX + radius;
                minY = centerY - radius;
                maxY = centerY + radius;
            }

            // Loop through the bounds and add coordinates to the list
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    coordinates.Add((x, y));
                }
            }

            return coordinates;
        }

        public static double GetManhattanDistance(OverlayTile start, OverlayTile neighbour, bool rand = false)
        {
            // Adds a bit of variation to the chosen path.
            double randMultiplier = 1;
            var x = UnityEngine.Random.Range(1, 120);
            if (x < 2)
                randMultiplier = UnityEngine.Random.Range(2f, 3f);

            //int dist = Convert.ToInt16(randMultiplier * (Math.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Math.Abs(start.gridLocation.y - neighbour.gridLocation.y)));
            
            double dist = (Math.Sqrt(Math.Pow(start.gridLocation.x - neighbour.gridLocation.x, 2) + Math.Pow(start.gridLocation.y - neighbour.gridLocation.y, 2)));
            if (rand)
            {
                 dist = randMultiplier * (Math.Sqrt(Math.Pow(start.gridLocation.x - neighbour.gridLocation.x, 2) + Math.Pow(start.gridLocation.y - neighbour.gridLocation.y, 2)));
            }
            return dist;
        }
    }
}

