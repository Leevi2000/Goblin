using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    // G, H, F are for pathfinding to store distances between start - tile and end - tile.
    public int G;
    public int H;

    public int F { get { return G + H; } }

    // isBlocked should be removed and replaced with tiletype string
    // to identify if a character with certain properties can pass on the tile. 
    public bool isBlocked;

    // This is replacement for isBlocked.
    public string tileType;
    public bool passable;
    public bool occupied;

    // Makes backtracking of tiles possible in pathfinder. 
    // Is used to construct the final path.
    public OverlayTile previous;

    public Vector3Int gridLocation;


    bool tileOnDebugMode = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    HideTile();
        //}

        if (!tileOnDebugMode)
        {
            Color col = gameObject.GetComponent<SpriteRenderer>().color;
            if (gameObject.GetComponent<SpriteRenderer>().color.a > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, col.a - 0.01f);
            }
        }
        
    }
    /// <summary>
    /// Shows white tile and disables debug flag on tile.
    /// </summary>
    public void ShowTile()
    {

        tileOnDebugMode = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    /// <summary>
    /// Enables debug flag on tile
    /// </summary>
    /// <param name="col"></param>
    public void SetColor(Color col)
    {
        tileOnDebugMode = true;
        gameObject.GetComponent<SpriteRenderer>().color = col;
    }

}
