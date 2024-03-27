using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDebugger : MonoBehaviour
{
    public Color COL_Occupied;
    public Color COL_CreaturePath;
    public Color COL_Reserved;

    public bool Show_Occupied;
    public bool Show_CreaturePath;
    public bool Show_Reserved;
    public bool Show_CreaturesOnTileList;

    public void EnablePath()
    {
        Show_Occupied = true;
        Show_CreaturePath = true;
        Show_Reserved = false;
        Show_CreaturesOnTileList = false;
    }

    public void EnableReserved()
    {
        Show_Occupied = false;
        Show_CreaturePath = false;
        Show_Reserved = true;
        Show_CreaturesOnTileList = false;
    }

    public void DisablePathDebug()
    {
        Show_Occupied = false;
        Show_CreaturePath = false;
        Show_Reserved = false;
        Show_CreaturesOnTileList = false;
        FadeOutAllTiles();
    }

    public void EnableLitCreatureTiles()
    {
        Show_Occupied = false;
        Show_CreaturePath = false;
        Show_Reserved = false;
        Show_CreaturesOnTileList = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Show_Occupied)
            ShowOccupiedTiles();

        if (Show_CreaturePath)
            ShowCreaturePaths();
       
        if(Show_Reserved)
            ShowReservedTiles();


        if (Show_CreaturesOnTileList)
            LitTileListTiles();

    }

    void LitTileListTiles()
    {
        var tiles = GetComponentsInChildren<OverlayTile>();

        foreach (var tile in tiles)
        {
            if (tile.creaturesOnTile.Count == 0)
            {
                tile.SetColor(Color.green);
            }

            if (tile.creaturesOnTile.Count == 1)
            {
                tile.SetColor(Color.yellow);
            }

            if (tile.creaturesOnTile.Count == 1)
            {
                tile.SetColor(Color.black);
            }


        }
    }

    void FadeOutAllTiles()
    {
        var tiles = GetComponentsInChildren<OverlayTile>();

        foreach (var tile in tiles)
        {
            tile.tileOnDebugMode = false;
        }
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

    void ShowReservedTiles()
    {
        var tiles = GetComponentsInChildren<OverlayTile>();

        foreach (var tile in tiles)
        {
            if (tile.reserved)
            {
                tile.SetColor(COL_Reserved);
            }
            else
            {
                tile.ShowTile();
            }
        }
    }

}
