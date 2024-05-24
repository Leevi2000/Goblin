using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    // Size is the length of structure's side. For example size 3 structure is a 3x3 building in game. 
    [SerializeField] private int size;
    [SerializeField] private int maxhp;
    [SerializeField] private int hp;

    [SerializeField] private string structureType;

    public string StructureType { get => structureType; set => structureType = value; }
    public int Size { get => size; set => size = value; }
}
