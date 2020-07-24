using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    public float jumpforce, movementSpeed;
    float forwardMovement, sideMovement;
    bool jump, jumped;
    Rigidbody body;
    Animator animationController;
    bool ready2Jump = false;
    public Material health;
    float r = 0;
    float g = 255;
    public float initial;


    void Start(){
        //Cursor.lockState = CursorLockMode.Locked;
        animationController = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
    }

    void Update(){

        if (Input.GetMouseButtonDown(0))
        {
            initial = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            float change = Time.time - initial;
            Debug.Log(change);
            if (change > 1f)
            {
                animationController.SetTrigger("attack");
                //transform.Translate(Vector3.forward * 5f);
            }
            else
            {
                animationController.SetTrigger("slash");

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            animationController.SetTrigger("block");
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            animationController.SetTrigger("letgo");
        }

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


        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
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
            Debug.Log("Grounded");
            ready2Jump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag.Equals("Ground")){
           Debug.Log("Not Grounded");
            animationController.ResetTrigger("jump");

            ready2Jump = false;
        }
    }
}
