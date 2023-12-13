using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class archerTargeting {
    /// <summary>
    /// Tristan Katcho
    /// 
    /// 
    /// Sphere collider to check if any users with this layer mask are nearby
    /// </summary>

    public static Enemy GetTarget(ArcherBehaviourComponent currentArcher)
    {
        Collider[] ennemiesInRange = Physics.OverlapSphere(currentArcher.transform.position, currentArcher.Range, currentArcher.EnemiesLayer);

        
        for(int i=0; i < ennemiesInRange.Length; i++)
        {
            Enemy currentEnemy = ennemiesInRange[i].GetComponent<Enemy>();//return first
            return currentEnemy;
        }

        return null;
    }
}
