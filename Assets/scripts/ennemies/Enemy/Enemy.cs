using System;
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
        if(ID == 0)
        {
            gameObject.GetComponent<HealthComponent>().SetHp(25);
        }
        if (ID == 2)
        {
            gameObject.GetComponent<HealthComponent>().SetHp(100);
        }
        else
        {
            gameObject.GetComponent<HealthComponent>().SetHp(50);
        }
    }
}
