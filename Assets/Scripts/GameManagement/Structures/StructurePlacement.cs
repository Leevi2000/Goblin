using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

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

    private void UpdateStructurePlacement(Structure structure)
    {

        foreach(var tile in overlayTiles)
        {
            if(tile != null)
            {
                tile.ShowTile();
            }
        }
        overlayTiles.Clear();

        var tilecoordinates = GenerateSquare(overlayTile.gridLocation.x, overlayTile.gridLocation.y, structure.Size);

        foreach(var coord in tilecoordinates)
        {
            var pos = new Vector2Int(coord.x, coord.y);
            var tile = MAP[pos];
            overlayTiles.Add(tile);
        }

        bool badTileFound = false;
        int height = overlayTile.gridLocation.z;
        foreach(var tile in overlayTiles)
        {
            if (tile != null)
            {
                if(tile.gridLocation.z != height)
                {
                    tile.SetColor(Color.red);
                    canPlace = false;
                    badTileFound = true;
                }
                else if(tile.tileType != overlayTile.tileType)
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

    public static List<(int x, int y)> GenerateSquare(int centerX, int centerY, int size)
    {
        List<(int x, int y)> coordinates = new List<(int x, int y)>();
 
        int radius = (int)Mathf.Round(size / 2);

        int minX = centerX - radius;
        int maxX = centerX + radius;
        int minY = centerY - radius;
        int maxY = centerY + radius;

        if (size % 2 == 0)
        {
            minX = centerX - radius + 1;
            maxX = centerX + radius;
            minY = centerY - radius + 1;
            maxY = centerY + radius;
        }
        else
        {
            // Calculate the bounds of the square
            minX = centerX - radius;
            maxX = centerX + radius;
            minY = centerY - radius;
            maxY = centerY + radius;
        }

        // Loop through the bounds and add coordinates to the list
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                coordinates.Add((x, y));
            }
        }

        return coordinates;
    }
    

}
