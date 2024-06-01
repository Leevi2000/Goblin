using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUILD { HOUSE, STORAGE }

[System.Serializable]
public class Keybinds
{
    public const KeyCode BUILD_HOUSE = KeyCode.Q;
    public const KeyCode BUILD_STORAGE = KeyCode.E;

    // THESE ARE FOR UNITY ISNPECTOR DRAG N DROP 
    BUILD house = BUILD.HOUSE;
    BUILD storage = BUILD.STORAGE;

    public const KeyCode CANCEL_SELECTION = KeyCode.Escape;

    public const KeyCode DBG_ENTER_BUILD_MODE = KeyCode.P;

    [SerializeField]
    public static IDictionary<BUILD, KeyCode> buildingKeybinds = new Dictionary<BUILD, KeyCode>() {
        { BUILD.HOUSE, BUILD_HOUSE},
        { BUILD.STORAGE, BUILD_STORAGE}
        };

    

}
