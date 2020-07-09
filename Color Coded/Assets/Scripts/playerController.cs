using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    public float jumpforce, movementSpeed;
    float moveVertical, moveHorizontal;
    bool jump;
    Rigidbody body;


    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        body = gameObject.GetComponent<Rigidbody>();
    }

    void Update(){
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
        transform.position += transform.forward * Time.deltaTime * (movementSpeed * moveVertical);
        transform.position += transform.right * Time.deltaTime * (movementSpeed * moveHorizontal);


        if (jump)
        {
            body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
    private void FixedUpdate(){

        
    }
}
