using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehaviourComponent : MonoBehaviour
{

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
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        fireElapsedTime += Time.deltaTime;
            Target = archerTargeting.GetTarget(this.gameObject.GetComponent<ArcherBehaviourComponent>());

        if (Target != null)
        {
            ArcherPivot.transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);

            if (fireElapsedTime >= Firerate)
            {
                if (Physics.Raycast(ArcherPivot.position, Target.gameObject.transform.position - ArcherPivot.position, out hit))
                    if (hit.collider.transform == Target.gameObject.transform)
                    {
                        animator.Play("shootBow");

                        ShootArrowWithDelay();
                        fireElapsedTime = 0;
                    }
            }
        }

    }
    public void ShootArrowWithDelay()
    {
        StartCoroutine(ShootArrowAfterDelay(1f)); // Wait for 2 seconds before shooting
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