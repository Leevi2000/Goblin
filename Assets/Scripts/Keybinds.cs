using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUILD { HOUSE, STORAGE }

[System.Serializable]
public class Keybinds
{
    public const KeyCode BUILD_HOUSE = KeyCode.Q;
    public const KeyCode BUILD_STORAGE = KeyCode.E;

    // THESE ARE FOR UNITY INSPECTOR DRAG N DROP 
    BUILD house = BUILD.HOUSE;
    BUILD storage = BUILD.STORAGE;

    public const KeyCode CANCEL_SELECTION = KeyCode.Escape;

    public const KeyCode DBG_ENTER_BUILD_MODE = KeyCode.P;

    // UI
    public const KeyCode OPEN_SELECTED_PANEL = KeyCode.B;


    public const KeyCode SELECT_CREATURE = KeyCode.Mouse0;
    public const KeyCode MOVE_SELECTED_CREATURES = KeyCode.Mouse1;
    public const KeyCode SELECT_ALL_CREATURES = KeyCode.Z;
    public const KeyCode SET_SELECTED_AS_WOODCUTTER = KeyCode.X;
    public const KeyCode SET_ALL_AS_UNEMPLOYED = KeyCode.C;


    [SerializeField]
    public static IDictionary<BUILD, KeyCode> buildingKeybinds = new Dictionary<BUILD, KeyCode>() {
        { BUILD.HOUSE, BUILD_HOUSE},
        { BUILD.STORAGE, BUILD_STORAGE}
        };

    

}
