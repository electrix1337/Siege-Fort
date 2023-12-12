using UnityEngine.AI;
using UnityEngine;

public class ChaseNode : Node
{
    private Transform mainTarget;
    private NavMeshAgent agent;
    private float buildingDetectionRadius;
    private LayerMask buildingLayer;

    public ChaseNode(Transform mainTarget, NavMeshAgent agent, float buildingDetectionRadius, LayerMask buildingLayer)
    {
        this.mainTarget = mainTarget;
        this.agent = agent;
        this.buildingDetectionRadius = buildingDetectionRadius;
        this.buildingLayer = buildingLayer;
    }

    public override NodeState Evaluate()
    {
        Transform nearestBuilding = BuildingDetector.GetNearestBuilding(agent.transform, buildingDetectionRadius, buildingLayer);

        Transform currentTarget = nearestBuilding != null ? nearestBuilding : mainTarget;

        float distance = Vector3.Distance(currentTarget.position, agent.transform.position);
        if (distance > 3f)
        {
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
            return NodeState.RUNNING;
        }
        else
        {
            //Debug.Log("reached");
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}
