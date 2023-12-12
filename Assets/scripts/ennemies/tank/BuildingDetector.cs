using UnityEngine;

public static class BuildingDetector
{
    public static Transform GetNearestBuilding(Transform seeker, float detectionRadius, LayerMask buildingLayer)
    {
        Collider[] buildingsInRange = Physics.OverlapSphere(seeker.position, detectionRadius, buildingLayer);

        Transform nearestBuilding = null;
        float nearestDistance = float.MaxValue;

        foreach (var buildingCollider in buildingsInRange)
        {
            float distance = Vector3.Distance(seeker.position, buildingCollider.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestBuilding = buildingCollider.transform;
            }
        }

        return nearestBuilding;
    }
}
