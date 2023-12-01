using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerShootingComponent : MonoBehaviour
{
    public Transform Orientation;
    public Transform SortieBullet;
    [SerializeField] float cooldown;
    [SerializeField] Animator animator;
    RaycastHit hit;
    public LayerMask InteractableLayers;

    float elapsedTime = 0;

    void Update()
    {
        //transform.SetPositionAndRotation(SortieBullet.position, Orientation.rotation);
        elapsedTime += Time.deltaTime;
        Debug.DrawRay(SortieBullet.position, hit.point);
        if (Physics.Raycast(SortieBullet.position, transform.forward, out hit, 1000, InteractableLayers) && elapsedTime >= cooldown)
            if (hit.collider.gameObject.layer == 7)
            {
                elapsedTime = 0;
                animator.Play("shootBow");
                Debug.Log("shoot");
            }
    }
}
