using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    public float jumpforce, movementSpeed;
    float forwardMovement, sideMovement;
    bool jump, jumped;
    Rigidbody body;
    public Animator animationController;
    bool ready2Jump = false;
    public float initial;


    void Start(){
        body = gameObject.GetComponent<Rigidbody>();
    }

    void Update(){
        forwardMovement = Input.GetAxis("Vertical");
        sideMovement = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
        if(forwardMovement > 0)
        {
            animationController.SetBool("run",true);
            animationController.SetFloat("runSpeed", forwardMovement);
        }else if (forwardMovement < 0)
        {
            animationController.SetBool("backUp", true);
            animationController.SetFloat("runSpeed", forwardMovement);
        }
        else if (forwardMovement == 0)
        {
            animationController.SetBool("run", false);
            animationController.SetBool("backUp", false);
            animationController.SetFloat("runSpeed", 0);
        }

        if (sideMovement > 0)
        {
            animationController.SetBool("right", true);
        }
        else if (sideMovement < 0)
        {
            animationController.SetBool("left", true);
        }
        else if (sideMovement == 0)
        {
            animationController.SetBool("left", false);
            animationController.SetBool("right", false);
        }
        transform.position += transform.forward * Time.deltaTime * (movementSpeed * forwardMovement);
        transform.position += transform.right * Time.deltaTime * (movementSpeed * sideMovement);

        
    }
    private void FixedUpdate()
    {
        if (jump && ready2Jump)
        {
            animationController.SetTrigger("jump");
            body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag.Equals("Ground")){
            ready2Jump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag.Equals("Ground")){
            animationController.ResetTrigger("jump");

            ready2Jump = false;
        }
    }
}
