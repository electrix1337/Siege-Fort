using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int health { get; private set; }
    CameraControlComponent camera;

    RectTransform healthBar;
    int maxHealth;

    private void Start()
    {
        camera = GameObject.Find(GameObjectPath.GetPath("Camera")).GetComponent<CameraControlComponent>();
    }

    //damage the building
    //return true if the entity is alive
    public bool TakeDamage(int amount)
    {
        health -= amount;
        //si le building n'a plus de vie, il est détruit
        if (health <= 0)
        {
            //Destroy(gameObject.transform.parent.gameObject);
            for (int i = 0; i < gameObject.transform.childCount; ++i)
            {
                Transform child = gameObject.transform.GetChild(i);
                if (child.name == "healthCanvas(Clone)")
                {
                    camera.RemoveRotatingUI(child.gameObject.GetComponent<activateOnRotation>());
                    break;
                }
            }
            Destroy(gameObject);
            return false;
        }
        else
        {
            ChangeUi();
            return true;
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
