using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goblin;
public class GoblinLoadTester : MonoBehaviour
{
    [SerializeField]
    public string a;
    [SerializeField]
    public Goblin.Goblin info;
    // Start is called before the first frame update
    void Start()
    {
        
        info = new Goblin.Goblin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGoblinData(Goblin.Goblin gob)
    {
        info = gob;
        a = info.FirstName + " " + info.LastName;
    }

}
