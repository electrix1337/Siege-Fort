using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BeahiourTreeManager : MonoBehaviour
{
    [SerializeField] Transform[] patrolDestinations;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform playerTransform;
    [SerializeField] float jumpDistance;

    private Node rootBT;

    private void Awake()
    {
        Vector3[] destinations = patrolDestinations.Select(t => t.position).ToArray();
        TaskBT[] tasks0 = new TaskBT[]
        {
            new Patrol(destinations, agent, playerTransform),
        };
        TaskBT[] tasks1 = new TaskBT[]
        {
            new Wait(4)
        };
        TaskBT[] tasks2 = new TaskBT[]
        {
            new ChaseBuilding(playerTransform, agent)
        };

        TaskNode patrolNode = new TaskNode("patrolNode1", tasks0);
        TaskNode waitNode = new TaskNode("taskNode1", tasks1);
        TaskNode chasePlayer = new TaskNode("chasePlayer1", tasks2);
        Node seq1 = new Sequence("seq1", new[] { patrolNode, chasePlayer, waitNode });

        rootBT = seq1;
    }

    void Update()
    {
        rootBT.Evaluate();
    }
}