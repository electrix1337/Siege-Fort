using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsTargetDetector : MonoBehaviour
{
    public static Transform GetNearestTarget(Transform seeker, float detectionRadius, LayerMask targetLayer)
    {
        Collider[] targetsInRange = Physics.OverlapSphere(seeker.position, detectionRadius, targetLayer);

        Transform nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (var targetCollider in targetsInRange)
        {
            float distance = Vector3.Distance(seeker.position, targetCollider.transform.position);
            if (distance < nearestDistance)
            {
                nearestTarget = targetCollider.transform;
                nearestDistance = distance;
            }
        }

        return nearestTarget;
    }
}
