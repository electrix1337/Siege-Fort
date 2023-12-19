using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyAi : MonoBehaviour
{
    /// <summary>
    /// Tristan Katcho
    /// </summary>
    [SerializeField] int damage;
    [SerializeField] float range;
    private NavMeshAgent agent;
    private Node topNode;
    private Animator animator; 
    
    public Transform mainTarget;
    public LayerMask buildingLayer;
    public float buildingDetectionRadius = 10f;
    public Transform currentTarget;
    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("Flag");
        if (gameObject.name.Contains("Team"))
        {
            targetObject = GameObject.FindGameObjectWithTag("EFlag");
        }
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        mainTarget = targetObject.transform;

        InitializeBehaviorTree();
    }

    void InitializeBehaviorTree()
    {
        ChaseNode chaseNode = new ChaseNode(mainTarget, agent, buildingDetectionRadius, buildingLayer,this);
        AttackNode attackNode = new AttackNode(agent, this, animator, damage, range);

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
