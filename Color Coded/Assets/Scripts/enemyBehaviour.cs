using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour { 

    public Animator animationController;
    public GameObject mainPlayer;
    public float moveSpeed, maxDistance, attackDistance;
    public static float health = 5f;
    public GameObject thisObject;



    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.LookAt(mainPlayer.transform);
        float distanceToPlayer = Vector3.Distance(transform.position, mainPlayer.transform.position);


        if (distanceToPlayer <= maxDistance && distanceToPlayer >= attackDistance) {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            animationController.SetBool("run", true);
        }
        else {
            animationController.SetBool("run", false);
        }
    }
    public void getHit(float damage) {
        health -= damage;
        if (health <= 0) {
            //Debug.Log("enemy dead");
            animationController.SetTrigger("die");

            Invoke("death", 3f);
        }
    }

    void death() {
        Debug.Log("enemy dead");
        thisObject.SetActive(false);
    }


}
