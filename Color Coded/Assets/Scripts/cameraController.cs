using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour{

    private const float min = -50.0f;
    private const float max = 0.0f;
    public Transform lookAtPlayer;
    public float cameraDistance; 
    public float x;
    public float z;
    public float currentX = 190f;
    public float currentY = -15.0f;
    public Transform body;
    bool mouse = true;


    private void Update(){
        Cursor.lockState = CursorLockMode.Confined;
        if (mouse)
        {
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");
        }
        
        currentY = Mathf.Clamp(currentY, min, max);
    }

    private void LateUpdate() {
        Vector3 dir = new Vector3(x, z, -cameraDistance);
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);

        transform.position = lookAtPlayer.position + rotation * dir;
        transform.LookAt(lookAtPlayer.position);
        transform.position = new Vector3(transform.position.x + x, transform.position.y + 1f, transform.position.z + z);

        Quaternion torque = Quaternion.Euler(0, currentX, 0);
        body.rotation = torque;

        if (Input.GetKeyDown("escape"))
        {
            mouse = false;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            mouse = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }
}
