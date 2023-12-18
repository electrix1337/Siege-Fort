using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(activateOnRotation))]
public class TimerActivateComponent : MonoBehaviour
{
    [SerializeField] float timerToActivate = 10;
    TextMeshProUGUI textMeshPro;
    float time;
    IActivateEnnemy iActivateEnnemy;

    private void Start()
    {
        iActivateEnnemy = gameObject.transform.parent.GetComponent<IActivateEnnemy>();
        textMeshPro  = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        time = timerToActivate;

        GameObject.Find(GameObjectPath.GetPath("Camera")).GetComponent<CameraControlComponent>().AddRotatingUI(
            gameObject.GetComponent<activateOnRotation>());
    }

    private void Update()
    {
        time -= Time.deltaTime;
        textMeshPro.text = "is active in: " + Mathf.FloorToInt(time);
        if (time <= 0)
        {
            timerToActivate = time;
            iActivateEnnemy.StartSpawning();
            gameObject.SetActive(false);
        }
    }
}
