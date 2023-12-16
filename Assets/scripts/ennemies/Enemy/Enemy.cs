using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    public float Speed;
    public int ID;

    public void Init()
    {
        Health = MaxHealth;
        
    }
    private void Start()
    {
        gameObject.GetComponent<HealthComponent>().SetHp(25);
        Debug.Log(gameObject.GetComponent<HealthComponent>().health);
    }
}
