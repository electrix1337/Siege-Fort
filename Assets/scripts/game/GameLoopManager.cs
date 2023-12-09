using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    //public static Vector3[] NodePositions;
    //public Transform NodeParent;

    private static Queue<int> EnemysToSummon;
    private static Queue<Enemy> EnemysToRemove;
    public bool LoopShouldEnd;

    private void Start()
    {
        EnemysToSummon = new Queue<int>();
        EnemysToRemove = new Queue<Enemy>();
        EntitySummon.Init();

        //NodePositions = new Vector3[NodeParent.childCount];

        //for(int i=0; i < NodePositions.Length;i++)
        //{
        //    NodePositions[i] = NodeParent.GetChild(i).position;
        //}

        StartCoroutine(GameLoop());
        InvokeRepeating("SummonTest",0f,1f);
        InvokeRepeating("RemoveTest", 0f, 2f);

    }
    void RemoveTest()
    {
        if (EntitySummon.EnemiesInGame.Count > 0)
        {
            EntitySummon.RemoveEnemy(EntitySummon.EnemiesInGame[Random.Range(0,EntitySummon.EnemiesInGame.Count)]);
        }
    }
    void SummonTest()
    {
        EnqueueEnemyIdSummon(1);
    }
    IEnumerator GameLoop()
    {
        while (!LoopShouldEnd)
        {
            //spawnEnemies
            if(EnemysToSummon.Count > 0)
            {
                for(int i =0; i < EnemysToSummon.Count; i++)
                {
                    EntitySummon.SummonEnemy(EnemysToSummon.Dequeue());
                }
            }




            //removeEnemies
            if (EnemysToSummon.Count > 0)
            {
                for (int i = 0; i < EnemysToRemove.Count; i++)
                {
                    EntitySummon.RemoveEnemy(EnemysToRemove.Dequeue());
                }
            }
            yield return null;
        }
    }
    public static void EnqueueEnemeyToRemove(Enemy enemy)
    {
        EnemysToRemove.Enqueue(enemy);
    }
    public static void EnqueueEnemyIdSummon(int ID)
    {
        EnemysToSummon.Enqueue(ID);
    }
}
