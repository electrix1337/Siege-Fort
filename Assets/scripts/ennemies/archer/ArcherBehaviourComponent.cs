using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehaviourComponent : MonoBehaviour
{
    /// <summary>
    /// Tristan Katcho
    /// </summary>

    public LayerMask EnemiesLayer;
    public Enemy Target;
    public Transform ArcherPivot;

    public Transform ArrowOut;

    public float Damage;
    public float Firerate;
    public float Range;
    public GameObject ArrowPrefab; 
    Animator animator;
    RaycastHit hit;

    private float fireElapsedTime = 0;
    private float fireDelay;
    void Start()
    {
        animator = GetComponent<Animator>();
        fireDelay = 1f / Firerate; // Calculate the delay based on fire rate
        UpdateAnimatorSpeed();

    }
    private void UpdateAnimatorSpeed()
    {
        float baseAnimationDuration = 1f; 
        float animationSpeed = baseAnimationDuration * Firerate;

        animator.speed = animationSpeed;
    }
    private void Update()
    {
        fireElapsedTime += Time.deltaTime;
        Target = archerTargeting.GetTarget(this.gameObject.GetComponent<ArcherBehaviourComponent>());

        if (Target != null)
        {
            ArcherPivot.transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);

            if (fireElapsedTime >= fireDelay)
            {
                if (Physics.Raycast(ArcherPivot.position, Target.gameObject.transform.position - ArcherPivot.position, out hit))
                    if (hit.collider.transform == Target.gameObject.transform)
                    {
                        animator.Play("shootBow");
                        Target.GetComponent<HealthComponent>().TakeDamage(10);
                        ShootArrowWithDelay();
                        fireElapsedTime = 0;
                    }
            }
        }

    }
    public void ShootArrowWithDelay()
    {
        StartCoroutine(ShootArrowAfterDelay(1f)); // just for it to be in sync with arrow coming out
    }
    private IEnumerator ShootArrowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        ShootArrow(); 
    }
    public void ShootArrow()
    {
        GameObject arrow = Instantiate(ArrowPrefab, ArrowOut.position, ArcherPivot.rotation);
        arrow.SetActive(true);
    }
}