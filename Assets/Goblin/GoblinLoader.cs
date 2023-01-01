using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinLoader : MonoBehaviour
{
    Database.Communicator.DatabaseCommunicator dbCommunicator = new Database.Communicator.DatabaseCommunicator();
    public GameObject testGoblin;
    Goblin.Goblin goblin = new Goblin.Goblin();

    // Start is called before the first frame update
    void Start()
    {
        goblin = testGoblin.GetComponent<Goblin.Goblin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGoblin()
    {
        goblin = dbCommunicator.LoadGoblin(9);
    }
}
