using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCalculationCounter : MonoBehaviour
{
    [SerializeField]
    public int counter = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            counter = 0;
        }
    }
}
