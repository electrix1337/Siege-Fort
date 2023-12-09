using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealthComponent : MonoBehaviour
{
    public int health { get; private set; }
    int maxHealth;

    //damage the building
    public void TakeDamage(int amount)
    {
        health -= amount;
        //si le building n'a plus de vie, il est détruit
        if (health < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            //change ui
        }
    }
    //heal the building
    public void Heal(int amount)
    {
        if (maxHealth != health)
        {
            health += amount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            //change ui
        }
    }
    //initialize the building hp
    public void SetBuildingHp(int hp)
    {
        health = hp;
        maxHealth = hp;
    }
}
