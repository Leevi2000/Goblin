using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using Helper.TilemapOperations;
public class StructurePlacement : MonoBehaviour
{
    [SerializeField]
    private bool placementEnabled = false;

    [SerializeField]
    private int gridSize = 3;

    // Current tile mouse is hovering on:
    OverlayTile overlayTile = new OverlayTile();

    // Overlaytiles that the structure would be standing on.
    List<OverlayTile> overlayTiles = new List<OverlayTile>();

    // Information about raycast from screen to mouse point
    RaycastHit2D? focusedTileHit = null;

    Dictionary<Vector2Int, OverlayTile> MAP;

    public Structure selectedStructure = null;
    public StructureSelector structureSelector;

    TileNames tileNames = new TileNames();


    bool canPlace = false;

    KeyCode pressedKey;

    [SerializeField]
    bool triggerInput = false;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Keybinds.DBG_ENTER_BUILD_MODE))
        {
            placementEnabled = !placementEnabled;
            MAP = MapManager.Instance.map;
        }

        if (placementEnabled)
        {
            GetFocusedOnTile();
            UpdateMouseOnTile();

            if(selectedStructure != null)
            {
                UpdateStructurePlacement(selectedStructure);

                if (Input.GetKeyDown(KeyCode.Mouse0) && canPlace)
                    PlaceStructure(selectedStructure.GetComponent<Structure>(), overlayTiles);
            }

        }


    }

    /// <summary>
    /// Changes placeable structures by listening to Keyboard Inputs.
    /// </summary>
    private void OnGUI()
    {
        bool keyInBinds = false;
        foreach(var item in Keybinds.buildingKeybinds)
        {
            if(item.Value == Event.current.keyCode)
            {
                keyInBinds = true;
            }
        }

        if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode != KeyCode.None)
        {
            pressedKey = Event.current.keyCode;
            if (pressedKey != Keybinds.DBG_ENTER_BUILD_MODE && keyInBinds)
            {
                selectedStructure = structureSelector.GetStructure(pressedKey);
                selectedStructure.BuildableTiles = tileNames.ReturnTilenamesByMovementType(selectedStructure.LandTypes);
                keyInBinds = false;
            }
        }
    }

    /// <summary>
    /// Updates the details about the tile mouse is pointing at.
    /// </summary>
    private void UpdateMouseOnTile()
    {
        focusedTileHit = GetFocusedOnTile();
        if (focusedTileHit == null)
            return;

        overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();

        transform.position = overlayTile.transform.position;

    }

    /// <summary>
    /// Creates given structure on an area on map. 
    /// </summary>
    /// <param name="structure"></param>
    /// <param name="occupiedTiles"></param>
    void PlaceStructure(Structure structure, List<OverlayTile> occupiedTiles)
    {
        //NOTE: There should be an reference to a structure on a Tile?


        OverlayTile bottomTile = occupiedTiles[0];

        foreach(var tile in overlayTiles)
        {
            tile.tileType = structure.StructureType;
        }

        structure.transform.position = bottomTile.transform.position;
        structure.GetComponent<SpriteRenderer>().sortingOrder = bottomTile.GetComponent<SpriteRenderer>().sortingOrder;
        Instantiate(structure);
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

        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

    /// <summary>
    /// Indicates the tiles the structure will be built on.
    /// Tiles' colors are changed on disallowed building area.
    /// </summary>
    /// <param name="structure"></param>
    private void UpdateStructurePlacement(Structure structure)
    {
        bool badTileFound = false;

        foreach (var tile in overlayTiles)
        {
            if(tile != null)
            {
                tile.ShowTile();
            }
        }

        // Get a list of tiles around cursor. Tile list contains tiles the selected structure would be built on.
        overlayTiles = TilemapHelper.GetTilesAroundTarget(overlayTile, structure.Size);

        // If overlayTile count is less than the structure takes space, placement goes over boundaries and shouldn't be placed.
        if(overlayTiles.Count != structure.Size*structure.Size)
        {
            foreach(var tile in overlayTiles)
            {
                tile.SetColor(Color.red);
            }
            canPlace = false;
            badTileFound = true;

            return;
        }

       
        int height = overlayTile.gridLocation.z;
        
        // Check tile building restrictions for each tile.
        foreach(var tile in overlayTiles)
        {
            if (tile != null)
            {
                // All tiles in a list should be on same level and on an allowed tile type. Preventing building on non-flat ground.
                if (!CheckIfAllowedTiletype(tile, structure) || tile.gridLocation.z != height) 
                {
                    tile.SetColor(Color.red);
                    canPlace = false;
                    badTileFound = true;
                }
                else
                {
                    tile.SetColor(Color.gray);
                    if(!badTileFound)
                        canPlace = true;
                }
            }
        }
    }

    /// <summary>
    /// Returns true if tiletype fulfills the structure's building requirements.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="structure"></param>
    /// <returns></returns>
    bool CheckIfAllowedTiletype(OverlayTile tile, Structure structure)
    {
        foreach(var tilename in structure.BuildableTiles)
        {
            if (tilename == tile.tileType)
            {
                return true;
            }
        }
        return false;
    }

    
    

}
