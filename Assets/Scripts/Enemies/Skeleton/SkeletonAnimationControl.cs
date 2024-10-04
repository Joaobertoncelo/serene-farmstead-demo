using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;
    private PlayerAnimation PlayerAnimation;
    private Skeleton skeleton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerAnimation = FindObjectOfType<PlayerAnimation>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    public void PlayAnimation(int value)
    {
        animator.SetInteger("Transition", value);
    }

    public void Attack()
    {
        if (!skeleton.IsDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                PlayerAnimation.OnHurt();
            }
        }
    }

    public void OnHurt()
    {
        if(skeleton.CurrentHealth <= 0)
        {
            skeleton.IsDead = true;
            animator.SetTrigger("Death");

            Destroy(skeleton.gameObject, 1f);
        }
        else
        {
            animator.SetTrigger("Hurt");
            skeleton.CurrentHealth--;

            skeleton.HealthBar.fillAmount = skeleton.CurrentHealth / skeleton.MaxHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
