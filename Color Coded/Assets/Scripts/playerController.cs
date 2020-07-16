using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    public float jumpforce, movementSpeed;
    float forwardMovement, sideMovement;
    bool jump;
    Rigidbody body;
    Animator animationController;
    bool ready2Jump = false;


    void Start(){
        //Cursor.lockState = CursorLockMode.Locked;
        animationController = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
        //controller = gameObject.AddComponent<CharacterController>();
    }

    void Update(){
        forwardMovement = Input.GetAxis("Vertical");
        sideMovement = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
        if(forwardMovement != 0)
        {
            animationController.SetBool("run",true);
            animationController.SetFloat("runSpeed", forwardMovement);
        }else if (forwardMovement == 0)
        {
            animationController.SetBool("run", false);
            animationController.SetFloat("runSpeed", 0);
        }
        transform.position += transform.forward * Time.deltaTime * (movementSpeed * forwardMovement);
        transform.position += transform.right * Time.deltaTime * (movementSpeed * sideMovement);


        if (jump && ready2Jump)
        {
            //animationController.SetTrigger("runJump");
            body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }if(!ready2Jump && body.velocity.y < 0)
        {
            body.velocity += Vector3.up * Physics.gravity.y * 2f * Time.deltaTime;
        }
        Debug.Log(ready2Jump);




        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag.Equals("Ground")){
            //Debug.Log("Grounded");
            ready2Jump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag.Equals("Ground")){
           // Debug.Log("Not Grounded");
           ready2Jump = false;
        }
    }
}
