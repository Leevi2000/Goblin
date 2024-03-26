using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructLibrary
{
    public struct ChunkTraverseOrientation
    {
        public ChunkTraverseOrientation(bool north, bool east, bool south, bool west)
        {
            North = north;
            East = east;
            South = south;
            West = west;
        }

        public bool North { get; }
        public bool East { get; }

        public bool South { get; }
        public bool West { get; }


    }
}
