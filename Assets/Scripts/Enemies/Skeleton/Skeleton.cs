using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    private float currentHealth;
    private bool isDead;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;

    [Header("Detection")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SkeletonAnimationControl skeletonAnimationControl;

    private Player player;
    private bool detectingPlayer;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public Image HealthBar { get => healthBar; set => healthBar = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && detectingPlayer)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                skeletonAnimationControl.PlayAnimation(2);
            }
            else
            {
                skeletonAnimationControl.PlayAnimation(1);
            }

            float position = player.transform.position.x - transform.position.x;
            if (position < 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (hit != null)
        {
            detectingPlayer = true;
        }
        else
        {
            detectingPlayer = false;
            skeletonAnimationControl.PlayAnimation(0);
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
