using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private GameObject camera;
    public float camSpeed = 1f;
    public float zoomSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        if (camera == null)
            Debug.Log("Camera couldn't be found");

       // camera.GetComponent<Camera>().usePhysicalProperties = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            camera.transform.position += new Vector3(0, camSpeed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            camera.transform.position += new Vector3(0, -camSpeed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            camera.transform.position += new Vector3(-camSpeed * Time.fixedDeltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            camera.transform.position += new Vector3(camSpeed * Time.fixedDeltaTime, 0);
        }

        if (Input.GetKey(KeyCode.N))
        {
            camera.GetComponent<Camera>().orthographicSize += zoomSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.M))
        {
            camera.GetComponent<Camera>().orthographicSize -= zoomSpeed * Time.fixedDeltaTime;
        }
    }
}
