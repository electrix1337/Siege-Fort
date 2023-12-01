using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerAimingComponent : MonoBehaviour
{
        [SerializeField] Transform target;
        [SerializeField] float speed;
        void Update()
        {
            Vector3 relativePos = target.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);
        }
}
