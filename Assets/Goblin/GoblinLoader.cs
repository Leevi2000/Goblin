using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinLoader : MonoBehaviour
{
    Database.Communicator.DatabaseCommunicator dbCommunicator = new Database.Communicator.DatabaseCommunicator();
    public GameObject testGoblin;
    Goblin.Goblin goblin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGoblin()
    {
        testGoblin.GetComponent<GoblinLoadTester>().UpdateGoblinData(dbCommunicator.LoadGoblin(9).InfoSender());
    }
}
