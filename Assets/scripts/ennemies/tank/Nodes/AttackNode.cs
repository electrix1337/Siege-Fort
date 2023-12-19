using UnityEngine.AI;
using UnityEngine;

public class AttackNode : Node
{
    /// <summary>
    /// Tristan Katcho
    /// </summary>

    private NavMeshAgent agent;
    private EnemyAi ai;
    private Animator animator; // Reference to the Animator component
    float elapsedTime = 0;
    HealthComponent targetHealth;
    public AttackNode(NavMeshAgent agent, EnemyAi ai, Animator animator)
    {
        this.agent = agent;
        this.ai = ai;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        elapsedTime += Time.deltaTime;
        float distanceToTarget = Vector3.Distance(agent.transform.position, ai.currentTarget.position);
        if (distanceToTarget <= 6) // Assuming ai.attackRange is the attack range
        {
            agent.isStopped = true;
            if (elapsedTime <= 2)
            {
                return NodeState.RUNNING;
            }
            else
            {
                elapsedTime = 0;
                animator.Play("punch");
                if (ai.currentTarget.tag.Contains("Flag"))
                {
                    Debug.Log("end game");
                    return NodeState.SUCCESS;
                }
                ai.currentTarget.gameObject.GetComponent<HealthComponent>().TakeDamage(5);
                return NodeState.SUCCESS;
            }
        }
        else
        {
            agent.isStopped = false;
            animator.Play("Running");
            return NodeState.FAILURE; // Target is out of range
        }
    }
}
