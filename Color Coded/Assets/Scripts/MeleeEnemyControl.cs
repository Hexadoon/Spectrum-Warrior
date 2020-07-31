using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyControl : enemyBehavior
{

    float attackTimer = 4f;
    float currentTime;
    bool attacking;
    public GameObject bouncer;
    public float attackPush;
    void Start()
    {
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, mainPlayer.transform.position);

        currentTime -= Time.deltaTime;

        if (distanceToPlayer <= attackDistance  && currentTime<=0f)
        {
            animationController.SetTrigger("attack");
            bouncer.transform.Translate(Vector3.forward * attackPush);
            attacking = true;
            Invoke("attack", 2f);
            currentTime = attackTimer;
        }
        if (attacking) {
            bouncer.transform.Translate(Vector3.forward * attackPush * Time.deltaTime);
        }


    }
    void attack()
    {
        attacking = false;
        bouncer.transform.Translate(Vector3.forward * -attackPush);

    }
    private void OnTriggerStay(Collider other) {
        if (other.tag.Equals("Player") && attacking) {
            playerCombat player = other.GetComponent<playerCombat>();
            player.getHitbyEnemy();
            attacking = false;
        }
    }

}
