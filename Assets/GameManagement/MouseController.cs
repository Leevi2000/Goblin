using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // These two should be removed
    //public GameObject characterPrefab;
    //private Creatures.Creature character;

    public List<Creatures.Creature> creatureList = new List<Creatures.Creature>();
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        var focusedTileHit = GetFocusedOnTile();


        if (focusedTileHit.HasValue)
        {
            OverlayTile overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
            transform.position = overlayTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder+1;


            // Select all creatures
            if (Input.GetKey(KeyCode.Z))
            {
                creatureList.Clear();
                var creatures = GameObject.FindGameObjectsWithTag("Creature");
                foreach (var creature in creatures)
                    creatureList.Add(creature.GetComponent<Creatures.Creature>());

                Debug.Log("Selected all creatures!");
            }

            //Find out how to register if raycast hits goblin
            if (Input.GetMouseButtonDown(0))
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    creatureList.Clear();
                }
                foreach(var creature in creatureList)
                {
                    Debug.Log(creature.name);
                }
                

                //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);
                //RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit, 1000);

                Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Creature")
                {
                    Creatures.Creature creature = hit.transform.gameObject.GetComponent<Creatures.Creature>();
                    creature.activeTile = overlayTile;
                    creatureList.Add(creature);
                }
                
               
            }

            //For moving / setting a command at mouse position
            if (Input.GetMouseButtonDown(1))
            {
                overlayTile.ShowTile();

                foreach (var creature in creatureList)
                {
                    creature.targetTile = overlayTile;
                    creature.pathRequest = true;
                }
                //// !!!!!!!! This is for testing purposes only !!!!!!!!!!!
                //// Remove null checker at some point!!
                //if (character == null)
                //{
                //    character = Instantiate(characterPrefab).GetComponent<Creatures.Creature>();
                //    PositionCharacterOnTile(overlayTile);
                //    character.activeTile = overlayTile;
                //} else
                //{
                //    character.targetTile = overlayTile;
                //    character.pathRequest = true;
                //}

            }

        }

    }

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
        character.activeTile = tile;
    }

}
