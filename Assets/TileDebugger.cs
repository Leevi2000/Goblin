using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDebugger : MonoBehaviour
{
    public Color COL_Occupied;
    public Color COL_CreaturePath;

    public bool Show_Occupied;
    public bool Show_CreaturePath;


    // Update is called once per frame
    void Update()
    {

        if (Show_Occupied)
            ShowOccupiedTiles();

        if (Show_CreaturePath)
            ShowCreaturePaths();
       
    }

    /// <summary>
    /// Changes tile color for occupied tiles. 
    /// </summary>
    void ShowOccupiedTiles()
    {
        var tiles = GetComponentsInChildren<OverlayTile>();

        foreach (var tile in tiles)
        {
            if (tile.occupied)
            {
                tile.SetColor(COL_Occupied);
            }
            else
            {
                tile.ShowTile();
            }
        }
    }

    /// <summary>
    /// Changes tile color to represent each characters pathfinding path.
    /// </summary>
    void ShowCreaturePaths()
    {
        var creatureController = GameObject.Find("GameManager").GetComponent<CreatureController>();
        foreach (var creature in creatureController.pathList.Keys)
        {
            List<OverlayTile> path = creatureController.pathList[creature];
            foreach (var tile in path)
            {
                tile.SetColor(COL_CreaturePath);
            }
        }
    }

}
