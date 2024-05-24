using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUILD { HOUSE }

[System.Serializable]
public class Keybinds
{
    public const KeyCode BUILD_HOUSE = KeyCode.Q;

    // THESE ARE FOR UNITY ISNPECTOR DRAG N DROP 
    BUILD house = BUILD.HOUSE;

    public const KeyCode CANCEL_SELECTION = KeyCode.Escape;

    public const KeyCode DBG_ENTER_BUILD_MODE = KeyCode.P;

    [SerializeField]
    public static IDictionary<BUILD, KeyCode> buildingKeybinds = new Dictionary<BUILD, KeyCode>() {
        { BUILD.HOUSE, BUILD_HOUSE}

        };

    

}
