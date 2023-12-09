using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int health { get; private set; }

    RectTransform healthBar;
    int maxHealth;

    //damage the building
    public void TakeDamage(int amount)
    {
        health -= amount;
        //si le building n'a plus de vie, il est détruit
        if (health <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            ChangeUi();
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
            ChangeUi();
        }
    }
    //initialize the building hp
    public void SetHp(int hp)
    {
        health = hp;
        maxHealth = hp;
        Debug.Log(hp);

        Transform canvas = null;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "healthCanvas(Clone)")
                canvas = gameObject.transform.GetChild(i);
        }
        healthBar = canvas.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
    }

    void ChangeUi()
    {
        healthBar.localPosition = new Vector3((healthBar.rect.width *
            ((float)health / maxHealth) - healthBar.rect.width) / 2, 0, 0);
        healthBar.localScale = new Vector3((float)health / maxHealth, 1, 1);
    }
}
