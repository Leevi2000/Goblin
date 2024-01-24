using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinLoader : MonoBehaviour
{
    Database.Communicator.DatabaseCommunicator dbCommunicator = new Database.Communicator.DatabaseCommunicator();
    public GameObject testGoblin;
    DTO.Goblin goblin;
    [SerializeField]
    string name;

    // Start is called before the first frame update
    void Start()
    {
        goblin = dbCommunicator.LoadGoblin(9).InfoSender();
        name = goblin.FirstName + "  " + goblin.LastName;

    }

    // Update is called once per frame
    void Update()
    {
        // sssss
    }
    
    public void LoadGoblin()
    {
        testGoblin.GetComponent<GoblinLoadTester>().UpdateGoblinData(dbCommunicator.LoadGoblin(9).InfoSender());
    }
}
