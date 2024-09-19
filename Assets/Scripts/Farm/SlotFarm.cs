using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private bool detecting;

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= 0)
        {
            spriteRenderer.sprite = hole;
        }

        //if (digAmount <= 0)
        //{
        //spriteRenderer.sprite = carrot;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel") && digAmount > 0)
        {
            OnHit();
        }
        
        if (collision.CompareTag("Water"))
        {
            detecting = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }
}
