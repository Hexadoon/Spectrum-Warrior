using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour {

    float initial;
    public Animator animationController;
    bool attacking = false;
    public float playerHealth = 80;
    public float regenTimer;
    public Material healthStatus, charm;
    playerController controller;
    Rigidbody body;
    float r = 0f;
    float g = 1f;
    float charmColor;
    bool reloading = true;
    bool blocking = true;

    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<playerController>();
        body = gameObject.GetComponent<Rigidbody>();
        healthStatus.SetVector("_Color", new Vector4(r, g, 0f));
        healthStatus.SetVector("_EmissionColor", new Vector4(r, g, 0f));
        charm.SetVector("_Color", new Vector4(1f, charmColor / regenTimer, 1f));
        charm.SetVector("_EmissionColor", new Vector4(1f, charmColor / regenTimer, 1f));
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            initial = Time.time;
        }
        if (Input.GetMouseButtonUp(0) && !attacking) {
            float change = Time.time - initial;
            if (change > 2f) {
                animationController.SetTrigger("combo");
                attacking = true;
                Invoke("disableAttack", 6);
            }
            else if (change > 1f) {
                animationController.SetTrigger("attack");
                attacking = true;
                Invoke("disableAttack", 4);
            }
            else {
                animationController.SetTrigger("slash");
                attacking = true;
                Invoke("disableAttack", 1f);
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            animationController.SetTrigger("block");

        }
        if (Input.GetMouseButtonUp(1)) {
            animationController.SetTrigger("letgo");
        }

        if (reloading) {
            charmColor -= Time.deltaTime;
            Debug.Log(charmColor);
            charm.SetVector("_Color", new Vector4(1f, charmColor / regenTimer, 1f));
            charm.SetVector("_EmissionColor", new Vector4(1f, charmColor / regenTimer, 1f));
            if (charmColor <= 0f) {
                reloading = false;
            }
        }

        if(!reloading && Input.GetKeyDown("e")) {
            charmColor = regenTimer;
            animationController.SetTrigger("heal");
            r = 0f;
            g = 1f;
            healthStatus.SetVector("_Color", new Vector4(r, g, 0f));
            healthStatus.SetVector("_EmissionColor", new Vector4(r, g, 0f));
            charm.SetVector("_Color", new Vector4(1f, charmColor / regenTimer, 1f));
            charm.SetVector("_EmissionColor", new Vector4(1f, charmColor / regenTimer, 1f));
            playerHealth = 80f;
            Invoke("reload",5f);
        }

    }
   
    void reload() {
        reloading = true;
    }

    void disableAttack() {
        attacking = false;
    }

    private void OnCollisionStay(Collision collision) {
        bool enemyhit = collision.collider.tag.Equals("Enemy");
        if (attacking && enemyhit) {
            enemyBehavior enemy = collision.collider.GetComponent<enemyBehavior>();
            enemy.getHit(20);
        }
    }

    private void OnTriggerStay(Collider collision) {
        bool enemyhit = collision.tag.Equals("Enemy");
        if (attacking && enemyhit) {
            enemyBehavior enemy = collision.GetComponent<enemyBehavior>();
            enemy.getHit(20);
        }
    }

    public void getHitbyEnemy() {
        playerHealth -= 10;
        Debug.Log("Got hit by Enemy"+ playerHealth);
        Invoke("pushBack", 0.5f);
        setHealth();
        if (playerHealth <= 0f) {
            controller.enabled = false;
        }
    }

    void pushBack() {
        body.AddForce(Vector3.forward * -20f, ForceMode.Impulse);
    }

    void setHealth() {
        if (g == 1f && r == 1f) {
            g -= 0.25f;
        } else if (r == 1f && g == 0f) {
            r = 1f;
        }else if (g == 1f) {
            r += 0.25f;
        } else if (r == 1f) {
            g -= 0.25f;
        }
        healthStatus.SetVector("_Color", new Vector4(r, g, 0f));
        healthStatus.SetVector("_EmissionColor", new Vector4(r, g, 0f));
    }
}
