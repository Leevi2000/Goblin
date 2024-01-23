using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goblin;
using Database.Backend;

public class DatabaseCommunicator : MonoBehaviour
{
    DB_Backend goblinData;

    // Start is called before the first frame update
    void Start()
    {
       DB_Backend goblinData = new DB_Backend();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
