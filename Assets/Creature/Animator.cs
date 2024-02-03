using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
   // private GameObject gameObject;
    private Creatures.Creature creature;


    private Vector2 oldPos;
    private Vector2 newPos;
    [SerializeField]
    private string pointingAt = "S";
    SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] directionSprites;


    // Start is called before the first frame update
    void Start()
    {
       // gameObject = this.GetComponent<GameObject>();
        creature = this.GetComponent<Creatures.Creature>();
        oldPos = this.transform.position;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (creature.Moving)
        {
            newPos = this.transform.position;
            pointingAt = CalculateDirection(oldPos, newPos);
            oldPos = newPos;

            UpdateSprite();
        }
    }



    private string CalculateDirection(Vector2 pos1, Vector2 pos2)
    {
        Vector2 newDir = (pos2 - pos1).normalized;

        double errorTolerance = 0.05;

        if (newDir.x > -errorTolerance && newDir.x < errorTolerance)
        {
            newDir.x = 0;
        }
        if (newDir.y > -errorTolerance && newDir.y < errorTolerance)
        {
            newDir.y = 0;
        }

        // If there's movement to the West only
        if (newDir.x < 0 && newDir.y == 0)
        {
            return "W";
        }

        // If there's movement to the North-West 
        if (newDir.x < 0 && newDir.y > 0)
        {
            return "NW";
        }

        // If there's movement to the North only
        if (newDir.x == 0 && newDir.y > 0)
        {
            return "N";
        }

        // If there's movement to the North-East
        if (newDir.x > 0 && newDir.y > 0)
        {
            return "NE";
        }


        // If there's movement to the East only
        if (newDir.x > 0 && newDir.y == 0)
        {
            return "E";
        }

        // If there's movement to the South-East
        if (newDir.x > 0 && newDir.y < 0)
        {
            return "SE";
        }


        // If there's movement to the South only
        if (newDir.x == 0 && newDir.y < 0)
        {
            return "S";
        }

        // If there's movement to the South-West
        if (newDir.x < 0 && newDir.y < 0)
        {
            return "SW";
        }

        return "S";
    }

    private void UpdateSprite()
    {
        Dictionary<string, int> map = new Dictionary<string, int>
        {
            { "S", 0 },
            { "SW", 1 },
            { "W", 2 },
            { "NW", 3 },
            { "N", 4 },
            { "NE", 5 },
            { "E", 6 },
            { "SE", 7 }
        };

        var spriteIndex = map[pointingAt];
        spriteRenderer.sprite = directionSprites[spriteIndex];
        
    }
}
