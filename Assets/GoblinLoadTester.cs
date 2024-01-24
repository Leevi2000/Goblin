using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GoblinLoadTester : MonoBehaviour
{
    [SerializeField]
    public string a;
    [SerializeField]
    public DTO.Goblin info;
    // Start is called before the first frame update
    void Start()
    {
        
        info = new DTO.Goblin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGoblinData(DTO.Goblin gob)
    {
        info = gob;
        a = info.FirstName + " " + info.LastName;
    }

}
