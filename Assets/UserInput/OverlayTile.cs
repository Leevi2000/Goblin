using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
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
    public OverlayTile previous;

    public Vector3Int gridLocation;

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    HideTile();
        //}
        Color col = gameObject.GetComponent<SpriteRenderer>().color;
        if (gameObject.GetComponent<SpriteRenderer>().color.a > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, col.a - 0.01f);
        }
    }

    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

}
