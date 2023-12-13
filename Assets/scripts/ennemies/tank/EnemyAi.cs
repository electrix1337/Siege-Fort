using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private NavMeshAgent agent;
    private Node topNode;
    private Animator animator; 

    public Transform mainTarget;
    public LayerMask buildingLayer;
    public float buildingDetectionRadius = 10f;
    public Transform currentTarget;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        InitializeBehaviorTree();
    }

    void InitializeBehaviorTree()
    {
        ChaseNode chaseNode = new ChaseNode(mainTarget, agent, buildingDetectionRadius, buildingLayer,this);
        AttackNode attackNode = new AttackNode(agent, this, animator);

        Sequence rootNode = new Sequence(new List<Node> { chaseNode, attackNode });
        topNode = rootNode;
    }

    void Update()
    {
        if (topNode != null)
        {
            topNode.Evaluate();
        }
    }

}
