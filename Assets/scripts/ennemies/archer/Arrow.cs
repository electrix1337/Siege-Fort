using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Speed = 10f;
    public Rigidbody Rigidbody;

    void Start()
    {
        Rigidbody.velocity = transform.forward * Speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); 

    }
}
