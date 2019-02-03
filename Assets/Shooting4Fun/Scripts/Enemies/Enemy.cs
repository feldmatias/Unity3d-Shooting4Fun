using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDeathable
{
    [Header ("Movement")]
    public float normalSpeed = 2f;
    public float searchingSpeed = 5f;

    [Header("Patrol")]
    public float patrolTime = 10f;
    public float patrolMinRadius = 20f;
    public float patrolMaxRadius = 80f;

    [Header("Search")]
    public float searchTime = 7f;
    public float searchDistance = 1f;
    public float searchRotationSpeed = 5f;

    [Header("FieldOfView")]
    public float fieldOfViewDistance = 10f;
    public int fieldOfViewAngle = 50;
    public float outOfFieldOfViewDistance = 3;

    protected NavMeshAgent navAgent;
    private Animator animator;
    protected PlayerSight target;

    private float patrolTimer = 0;
    private float searchTimer;
    private Vector3? lastSightingPosition;
    protected bool isAttacking = false;
    protected bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        target = AssetsManager.Instance.Player.GetComponent<PlayerSight>();
        animator = GetComponentInChildren<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.Warp(transform.position);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        isAttacking = false;
        if (isDead){
            Stop();
            return;
        }

        if (TargetInFieldOfView()) {
            isAttacking = true;
            AttackTarget();
        } else if (lastSightingPosition.HasValue){
            SearchLastSighting();
        } else {
            Patrol();
        }
    }

    private void LateUpdate()
    {
        Animate();
    }

    private Vector3 GetRandomDestination()
    {
        var direction = Random.insideUnitSphere * Random.Range(patrolMinRadius, patrolMaxRadius);
        direction.y = 0;
        return transform.position + direction;
    }

    private void Animate()
    {
        animator.SetFloat(EnemyAnimationTags.PLANE_VELOCITY, navAgent.velocity.magnitude);
    }

    private bool TargetInFieldOfView()
    {
        var distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > fieldOfViewDistance){
            return false;
        } else if (distance <= outOfFieldOfViewDistance) {
            return true;
        }

        var targetDir = target.transform.position - transform.position;
        targetDir.y = 0;
        var angle = Vector3.Angle(targetDir, transform.forward);
        if (angle > fieldOfViewAngle){
            return false;
        }

        foreach (var targetPoint in target.importantPoints){
            RaycastHit hit;
            var direction = (targetPoint.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, direction, out hit)){
                if (hit.transform.gameObject.tag == Tags.PLAYER){
                    return true;
                }
            }

        }

        return false;
    }

    protected virtual void AttackTarget()
    {
        transform.forward = (target.transform.position - transform.position).normalized;

        lastSightingPosition = target.transform.position;
        searchTimer = searchTime;
        patrolTimer = patrolTime;
    }

    private void SearchLastSighting()
    {
        var distanceToTarget = Vector3.Distance(transform.position, lastSightingPosition.Value);
        if (distanceToTarget < searchDistance){
            Stop();
            searchTimer -= Time.deltaTime;
            if (searchTimer > 0){
                //Continue searching in position for target
                RotateToSearch();
            } else {
                //Finished searching
                searchTimer = searchTime;
                lastSightingPosition = null;
            }
        } else {
            //Didn't reach search point
            Move(searchingSpeed);
            navAgent.SetDestination(lastSightingPosition.Value);
        }
    }

    private void Patrol()
    {
        patrolTimer -= Time.deltaTime;
        Move(normalSpeed);

        if (patrolTimer <= 0)
        {
            patrolTimer = patrolTime;

            navAgent.SetDestination(GetRandomDestination());
        }
    }

    protected void Stop()
    {
        navAgent.speed = 0;
        navAgent.isStopped = true;
    }

    protected void Move(float speed)
    {
        navAgent.speed = speed;
        navAgent.isStopped = false;
    }

    private void RotateToSearch()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.right), searchRotationSpeed * Time.deltaTime);
    }

    public void BulletDetected(Vector3 position)
    {
        lastSightingPosition = position;
        searchTimer = searchTime;
    }

    public void Die()
    {
        isDead = true;
        GetComponent<Collider>().enabled = false;
        animator.SetTrigger(EnemyAnimationTags.DEAD_TRIGGER);
        EnemyManager.Instance.EnemyKilled();
    }

    public bool IsDead()
    {
        return isDead;
    }
}
