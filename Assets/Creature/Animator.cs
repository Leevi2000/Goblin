/* Animator Script
 * 
 * Changes creature sprites and equipments based on current location.
 * 
 * Handling certain action animations should be added later in development.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{

    private Creatures.Creature creature;

    // Used for determining character rotation
    // -------------------
    private Vector2 oldPos;
    private Vector2 newPos;
    // -------------------

    // Default pointing direction is South
    [SerializeField]
    private string pointingAt = "S";

    // Body sprites
    SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] directionSprites;

    // Armor sprites
    SpriteRenderer armorSpriteHelmet;
    SpriteRenderer armorSpriteChestplate;
    SpriteRenderer armorSpriteTrousers;

    SpriteRenderer itemSprite;


    // Start is called before the first frame update
    void Start()
    {
        //
        creature = this.GetComponent<Creatures.Creature>();
        oldPos = this.transform.position;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        ApplyRotationChanges();
    }


    /// <summary>
    /// Calculates direction changes between two vector positions and
    /// returns movement orientation.
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Updates sprite changes
    /// </summary>
    private void UpdateSprite()
    {
        // NOTE: Maybe use struct instead?
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

        // Changing sprite based on direction
        var spriteIndex = map[pointingAt];
        spriteRenderer.sprite = directionSprites[spriteIndex];
    }

    /// <summary>
    /// If creature is moving, observe changes in direction and
    /// and change sprite respectively.
    /// </summary>
    private void ApplyRotationChanges()
    {
        if (creature.Moving)
        {
            newPos = this.transform.position;
            pointingAt = CalculateDirection(oldPos, newPos);
            oldPos = newPos;

            UpdateSprite();
        }
    }

}
