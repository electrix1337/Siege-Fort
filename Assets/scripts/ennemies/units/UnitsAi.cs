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
        currentTarget = FindPriorityTarget();

        if (currentTarget != null)
        {
            MoveTowardsTarget(currentTarget);

            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                if (Time.time > lastAttackTime + 1f / attackRate)
                {
                    AttackTarget(currentTarget);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void MoveTowardsTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }

    void AttackTarget(Transform target)
    {
        Debug.Log("attack");
        //degats + anims
    }

    Transform FindPriorityTarget()
    {
        Transform enemyTarget = UnitsTargetDetector.GetNearestTarget(transform, detectionRadius, enemyLayer);
        if (enemyTarget != null)
            return enemyTarget;

        Transform buildingTarget = BuildingDetector.GetNearestBuilding(transform, detectionRadius, buildingLayer);
        if (buildingTarget != null)
            return buildingTarget;


        return mainTarget;
    }
}
