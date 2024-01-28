using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNames
{
    // This is supposed to serve as an module to ease adding new tile types
    // and simplify path type logic to work as tiletypes rather than tile names.
    // This way a creature can be easily set to have certain movement types, and
    // tile names will be taken into the needed list by the provided method.

    string[] trees = {
        "tree_1"
    };

    string[] lands = {
        "grass", "grass_slab"
    };

    string[] water = {
        "water"
    };

    /// <summary>
    /// Returns a set of tilenames a creature with a certain movement type can travel on.
    /// </summary>
    /// <param name="movementTypes"></param>
    /// <returns></returns>
    public List<string> ReturnTilenamesByMovementType(List<string> movementTypes)
    {
        List<string> tileNames = new List<string>();

        foreach (string type in movementTypes)
        {
            switch(type)
            {
                case "land":
                    tileNames.AddRange(lands);
                    break;

                case "water":
                    tileNames.AddRange(water);
                    break;

                case "air":
                    tileNames.AddRange(lands);
                    tileNames.AddRange(water);
                    tileNames.AddRange(trees);
                    break;
            }
        }

        return tileNames;
    }
}
