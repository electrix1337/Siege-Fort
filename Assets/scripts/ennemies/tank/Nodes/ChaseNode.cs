using UnityEngine.AI;
using UnityEngine;

public class ChaseNode : Node
{
    /// <summary>
    /// Tristan Katcho
    /// </summary>

    private Transform mainTarget;
    private NavMeshAgent agent;
    private float buildingDetectionRadius;
    private LayerMask buildingLayer;
    private EnemyAi ai;

    public ChaseNode(Transform mainTarget, NavMeshAgent agent, float buildingDetectionRadius, LayerMask buildingLayer,EnemyAi ai)
    {
        this.mainTarget = mainTarget;
        this.agent = agent;
        this.buildingDetectionRadius = buildingDetectionRadius;
        this.buildingLayer = buildingLayer;
        this.ai = ai;

    }

    public override NodeState Evaluate()
    {
        Transform nearestBuilding = BuildingDetector.GetNearestBuilding(agent.transform, buildingDetectionRadius, buildingLayer);
        Transform currentTarget = nearestBuilding != null ? nearestBuilding : mainTarget;
        ai.currentTarget = currentTarget;
        float distance = Vector3.Distance(currentTarget.position, agent.transform.position);
        if (distance > 6f)
        {
            agent.SetDestination(currentTarget.position);
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS; 
        }
    }
}
