using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator animator;
    private CastingArea castingArea;

    private bool isHurt;
    private float recoveryTime = 1.5f;
    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        castingArea = FindObjectOfType<CastingArea>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnCut();
        OnDigging();
        OnWatering();
        
        if(isHurt)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= recoveryTime)
            {
                isHurt = false;
                timeCount = 0f;
            }
        }
    }

    #region Movement
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if (player.isRolling && !animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
            {
                animator.SetTrigger("Roll");
                player.isRolling = false;
            }
            else if(player.isRunning)
            {
                animator.SetInteger("Transition", 2);
            }
            else
            {
                animator.SetInteger("Transition", 1);
            }
        }
        else
        {
            animator.SetInteger("Transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    #endregion

    #region Actions

    void OnCut()
    {
        if(player.isCutting)
        {
            animator.SetInteger("Transition", 3);
        }
    }

    void OnDigging()
    {
        if (player.isDigging)
        {
            animator.SetInteger("Transition", 4);
        }
    }
    
    void OnWatering()
    {
        if (player.isWatering)
        {
            animator.SetInteger("Transition", 5);
        }
    }

    public void OnCastingStarted()
    {
        animator.SetTrigger("Casting");
        player.isPaused = true;
    }

    public void OnCastingStopped()
    {
        castingArea.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        animator.SetBool("Hammering", true);
    }

    public void OnHammeringEnded()
    {
        animator.SetBool("Hammering", false);
    }
    
    public void OnHurt()
    {
        if (!isHurt)
        {
            animator.SetTrigger("Hurt");
            isHurt = true;
        }
    }

    #endregion

    #region Battle

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            hit.GetComponentInChildren<SkeletonAnimationControl>().OnHurt();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion
}
