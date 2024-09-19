using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnCut();
        OnDigging();
        OnWatering();
    }

    #region Movement
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if (player.isRolling)
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

    

    #endregion
}
