using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(new Vector2(0, speed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(new Vector2(0, -speed));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(new Vector2(-speed, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(new Vector2(speed, 0));
        }
    }
}
