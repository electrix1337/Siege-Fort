using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerActivateComponent : MonoBehaviour
{
    [SerializeField] float timerToActivate = 10;
    float time = 0;
    IActivateEnnemy iActivateEnnemy;

    private void Start()
    {
        iActivateEnnemy = gameObject.transform.parent.GetComponent<IActivateEnnemy>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timerToActivate)
        {
            timerToActivate = time;
            iActivateEnnemy.StartSpawning();
            gameObject.SetActive(false);
        }
    }
}
