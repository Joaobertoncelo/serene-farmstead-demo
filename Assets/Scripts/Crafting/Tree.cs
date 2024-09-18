using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalwood;

    [SerializeField] private ParticleSystem leafs;

    public void OnHit()
    {
        treeHealth--;

        animator.SetTrigger("isHit");
        leafs.Play();

        if(treeHealth < 1)
        {
            for(int i = 0; i < totalwood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), transform.rotation);
            }
            animator.SetTrigger("cut");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe"))
        {
            OnHit();
        }
    }
}
