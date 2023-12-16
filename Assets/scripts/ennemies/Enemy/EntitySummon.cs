using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class EntitySummon : MonoBehaviour
{
    public static List<Enemy> EnemiesInGame;
    public static List<Transform> EnemiesInGameTransform;
    public static Dictionary<int, GameObject> EnemyPrefabs;
    public static Dictionary<int, Queue<Enemy>> EnemyObjectPool;

    private static bool exists = false;
    public static void Init()
    {
        if (!exists)
        {
            EnemyPrefabs = new Dictionary<int, GameObject>();
            EnemyObjectPool = new Dictionary<int, Queue<Enemy>>();
            EnemiesInGameTransform = new List<Transform>();
            EnemiesInGame = new List<Enemy>();


            EnemySummonData[] Enemies = Resources.LoadAll<EnemySummonData>("Enemies");

            foreach (EnemySummonData enemy in Enemies)
            {
                EnemyPrefabs.Add(enemy.EnemyID, enemy.enemyPrefab);
                EnemyObjectPool.Add(enemy.EnemyID, new Queue<Enemy>());
            }

            exists = true;
        }
        else
        {
            Debug.Log("ENTITY ALR INITIALIZED");
        }

    }

    public static Enemy SummonEnemy(int EnemyID)
    {
        Enemy SummonedEnemy = null;

        if (EnemyPrefabs.ContainsKey(EnemyID))
        {
            Queue<Enemy> queue = EnemyObjectPool[EnemyID];

            if (queue.Count > 0)
            {
                SummonedEnemy = queue.Dequeue();
                SummonedEnemy.Init();

                SummonedEnemy.gameObject.SetActive(true);
                Debug.Log("Enemie added");

            }
            else
            {
                GameObject Enemmy = Instantiate(EnemyPrefabs[EnemyID],Vector3.zero,Quaternion.identity);
                SummonedEnemy = Enemmy.GetComponent<Enemy>();
                SummonedEnemy.Init();
            }
        }
        else
        {
            Debug.Log("ENTITY ID DOES NOT EXIST");
        }

        EnemiesInGameTransform.Add(SummonedEnemy.transform);
        EnemiesInGame.Add(SummonedEnemy);
        SummonedEnemy.ID = EnemyID;
        return SummonedEnemy;
    }
    public static void RemoveEnemy(Enemy enemytoRemove)
    {
        EnemyObjectPool[enemytoRemove.ID].Enqueue(enemytoRemove);
        enemytoRemove.gameObject.SetActive(false);
        EnemiesInGameTransform.Remove(enemytoRemove.transform);

        Debug.Log("Enemie removed");
        EnemiesInGame.Remove(enemytoRemove);
    }
}
