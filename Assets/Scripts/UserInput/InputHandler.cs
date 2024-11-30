using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gamestate { DEFAULT, UI_OPENED }

public class InputHandler : MonoBehaviour
{
    [SerializeField] MouseController mouseController;
    KeyCode pressedKey = KeyCode.None;

    private void Start()
    {
        
    }

    /// <summary>
    /// Changes placeable structures by listening to Keyboard Inputs.
    /// </summary>
    private void OnGUI()
    {
        if (Event.current.type == EventType.KeyDown)
            Debug.Log("Keydown");

        if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode != KeyCode.None)
            Debug.Log("Key Pressed");

        if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.keyCode != KeyCode.None)
            Debug.Log("Mouse Pressed");


        if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode != KeyCode.None)
        {
            pressedKey = Event.current.keyCode;
        }
    }


    private void ProcessInput(KeyCode key, Gamestate state = Gamestate.DEFAULT)
    {
        if (Gamestate.DEFAULT == state)
            if (key == Keybinds.MOVE_SELECTED_CREATURES)
                mouseController.MoveSelected();

            if (key == Keybinds.MOVE_SELECTED_CREATURES)
                mouseController.MoveSelected();
    }
}
