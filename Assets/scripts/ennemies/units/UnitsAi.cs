using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitsAi : MonoBehaviour
{
    private NavMeshAgent agent;
    private Node topNode;
    private Animator animator;

    public Transform mainTarget;
    public LayerMask enemyLayer;
    public LayerMask buildingLayer;
    public float detectionRadius = 10f;
    public Transform currentTarget;

    Transform target = null;
    HealthComponent targetHealth;
    


    public float attackRange = 5f;
    public float attackRate = 1f;
    private float lastAttackTime;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (target == null)
        {
            target = FindPriorityTarget();
        }
        if (target != null)
        {
            MoveTowardsTarget(target);

            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                if (Time.time > lastAttackTime + 1f / attackRate)
                {
                    AttackTarget();
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void MoveTowardsTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        Debug.Log("attack");
        //degats + anims
        bool isalive = targetHealth.TakeDamage(10);
        Debug.Log(targetHealth.health);
        if (!isalive)
            target = null;
    }

    Transform FindPriorityTarget()
    {
        Transform enemyTarget = UnitsTargetDetector.GetNearestTarget(transform, detectionRadius, enemyLayer);
        if (enemyTarget != null)
        {
            targetHealth = enemyTarget.GetComponent<HealthComponent>();
            return enemyTarget;
        }

        Transform buildingTarget = BuildingDetector.GetNearestBuilding(transform, detectionRadius, buildingLayer);
        if (buildingTarget != null)
        {
            targetHealth = enemyTarget.GetComponent<HealthComponent>();
            return buildingTarget;
        }


        return mainTarget;
    }
}
