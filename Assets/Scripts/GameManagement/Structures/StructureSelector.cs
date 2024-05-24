using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSelector : MonoBehaviour
{
    [System.Serializable]
    public struct StructureBind
    {
        public GameObject structurePrefab;
        public BUILD keybindId;

        StructureBind(GameObject structurePrefab, BUILD keybind)
        {
           this.structurePrefab = structurePrefab;
           this.keybindId = keybind;
        }
    }
    
    [SerializeField]
    private List<StructureBind> structures;

    public Structure GetStructure(KeyCode keybind)
    {
        foreach(StructureBind item in structures)
        {
            var itemKeybind = Keybinds.buildingKeybinds[item.keybindId];
            if (itemKeybind == keybind)
            {
                return item.structurePrefab.GetComponent<Structure>();
            }
        }

        return null;
    }
    

}
