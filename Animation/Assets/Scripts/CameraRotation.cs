using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    float rotationspeed = 5.0f;
    // Use this for initialization
    void Start () {
        //print(transform);
        //Camera camera = GetComponentInChildren<Camera>();
        //print(camera.transform);
	}
	
	// Update is called once per frame
	void Update () {
 
        float mouseX = Input.GetAxis("Mouse X") * rotationspeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationspeed;
        transform.rotation = Quaternion.Euler(0, mouseX, 0) * transform.rotation;
        Camera camera = GetComponentInChildren<Camera>();
        camera.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0) * camera.transform.localRotation;
    }
}
