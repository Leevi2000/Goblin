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

    // For certain occasions, for optimization:
    public Creatures.Creature reservedTo;
    public bool reserved;

    const float RESERVATION_TIME = 5;
    public float reservationTimer;

    // Makes backtracking of tiles possible in pathfinder. 
    // Is used to construct the final path.
    public OverlayTile previous;

    public Vector3Int gridLocation;


    bool tileOnDebugMode = false;

    private void Start()
    {
        reservationTimer = RESERVATION_TIME;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(reserved)
        {
            ReservationTimer();
        }

        if (!tileOnDebugMode)
        {
            FadeOutTile();
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
    /// Enables debug flag on tile.
    /// </summary>
    /// <param name="col"></param>
    public void SetColor(Color col)
    {
        tileOnDebugMode = true;
        gameObject.GetComponent<SpriteRenderer>().color = col;
    }

    /// <summary>
    /// If tile has been reserved for too long, make it available again.
    /// </summary>
    public void ReservationTimer()
    {
        reservationTimer = reservationTimer - Time.deltaTime;

        if (reservationTimer < 0)
        {
            reservationTimer = RESERVATION_TIME;
            reserved = false;
        }
    }

    /// <summary>
    /// Fades tile colour over time
    /// </summary>
    public void FadeOutTile()
    {
        Color col = gameObject.GetComponent<SpriteRenderer>().color;
        if (gameObject.GetComponent<SpriteRenderer>().color.a > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, col.a - 0.01f);
        }
    }
        

}
