using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ShootingEnemyDirections { LEFT = -1, RIGHT = 1 };

public class ShootingEnemy : Enemy
{

    [Header("Shooting enemy")]
    public ShootingEnemyAim enemyAim;
    public ShootingEnemyWeapon weapon;
    public float attackingMovementSpeed;
    public float attackingDirectionTimeout = 7;

    private ShootingEnemyDirections movingDirection = ShootingEnemyDirections.LEFT;
    private float attackingTimer = 0;

    protected override void Update()
    {
        base.Update();
        enemyAim.SetIsAiming(isAttacking);
        weapon.IsAttacking = isAttacking;

        if (isAttacking)
        {
            MoveAttack();
        }
    }

    private void MoveAttack()
    {
        attackingTimer += Time.deltaTime;
        if (attackingTimer >= attackingDirectionTimeout){
            attackingTimer = 0;
            ChangeAttackDirection();
        }

        Move(attackingMovementSpeed);
        navAgent.SetDestination(transform.position + transform.right * (int)movingDirection * patrolMinRadius);
    }

    private void ChangeAttackDirection()
    {
        movingDirection = movingDirection == ShootingEnemyDirections.LEFT ? ShootingEnemyDirections.RIGHT : ShootingEnemyDirections.LEFT;
    }
}
