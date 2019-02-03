using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    [Header("Exploding Enemy")]
    public float distanceToExplode = 1;
    public float attackSpeed = 8;
    public GameObject explosion;

    protected override void Update()
    {
        base.Update();
        if (isAttacking){
            Attack();
        }
    }

    private void Attack()
    {
        var distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= distanceToExplode) {
            Explode();
        } else {
            Move(attackSpeed);
            navAgent.SetDestination(target.transform.position);
        }
    }

    private void Explode()
    {
        if (!isDead) {
            Stop();
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }

            explosion.SetActive(true);
            Die();
        }
    }
}
