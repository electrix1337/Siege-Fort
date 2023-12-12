using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackNode : Node
{
    private NavMeshAgent agent;
    private EnemyAi ai;
    private Animator animator; // Reference to the Animator component

    public AttackNode(NavMeshAgent agent, EnemyAi ai, Animator animator)
    {
        this.agent = agent;
        this.ai = ai;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = true; 
        animator.Play("Punch");

        Debug.Log("coucou");
        // Play animations + damage

        return NodeState.SUCCESS;
    }
}
