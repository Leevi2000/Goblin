using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // List of creatures currently selected.
    public List<Creatures.Creature> creatureList = new List<Creatures.Creature>();

    // Current tile mouse is hovering on:
    OverlayTile overlayTile = new OverlayTile();

    // Information about raycast from screen to mouse point
    RaycastHit2D? focusedTileHit = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        UpdateMouseOnTile();

        if (focusedTileHit.HasValue)
        {
            // Select all creatures
            if (Input.GetKey(KeyCode.Z))
                SelectAllCreatures();
            

            //Find out how to register if raycast hits goblin
            if (Input.GetMouseButtonDown(0))
                SelectCreature();
            

            //For moving / setting a command at mouse position
            if (Input.GetMouseButtonDown(1))
                MoveSelected();
            

        }

    }

    /// <summary>
    /// Shoots a raycast on mouse position to get a tile to focus on.
    /// </summary>
    /// <returns></returns>
    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if(hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        
        return null;
    }

    private void PositionCharacterOnTile(OverlayTile tile, Creatures.Creature character)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        character.ActiveTile = tile;
    }

    /// <summary>
    /// Selects all creatures. Mainly for testing purposes.
    /// </summary>
    private void SelectAllCreatures()
    {
        creatureList.Clear();
        var creatures = GameObject.FindGameObjectsWithTag("Creature");
        foreach (var creature in creatures)
            creatureList.Add(creature.GetComponent<Creatures.Creature>());

        Debug.Log("Selected all creatures!");
    }

    /// <summary>
    /// Selects creature/creatures adding them to creaturelist
    /// </summary>
    private void SelectCreature()
    {

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            creatureList.Clear();
        }
        foreach (var creature in creatureList)
        {
            Debug.Log(creature.name);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 1000);

        Debug.Log(hit.transform.tag);
        if (hit.transform.tag == "Creature")
        {
            Creatures.Creature creature = hit.transform.gameObject.GetComponent<Creatures.Creature>();
            creature.ActiveTile = overlayTile;
            creatureList.Add(creature);
        }

    }

    /// <summary>
    /// Updates the details about the tile mouse is pointing at.
    /// </summary>
    private void UpdateMouseOnTile()
    {
        focusedTileHit = GetFocusedOnTile();
        overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
        transform.position = overlayTile.transform.position;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder + 1;
    }

    
    private void MoveSelected()
    {
        overlayTile.ShowTile();

        foreach (var creature in creatureList)
        {
            creature.TargetTile = overlayTile;
            creature.PathRequest = true;
        }
    }
}
