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

    public OverlayTile previous;

    public Vector3Int gridLocation;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HideTile();
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
