using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySummonData",menuName = "Create EnemySummonData")]
public class EnemySummonData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int EnemyID;
}
