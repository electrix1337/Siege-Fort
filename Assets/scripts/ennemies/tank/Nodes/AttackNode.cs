using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    int damage;
    float range;
    public AttackNode(NavMeshAgent agent, EnemyAi ai, Animator animator, int damage, float range)
    {
        this.agent = agent;
        this.ai = ai;
        this.animator = animator;
        this.range = range;
        this.damage = damage;
    }

    public override NodeState Evaluate()
    {
        elapsedTime += Time.deltaTime;
        float distanceToTarget = Vector3.Distance(agent.transform.position, ai.currentTarget.position);
        if (distanceToTarget <= range) // Assuming ai.attackRange is the attack range
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
                    SceneManager.LoadScene(1);
                    return NodeState.SUCCESS;
                }
                ai.currentTarget.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
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
