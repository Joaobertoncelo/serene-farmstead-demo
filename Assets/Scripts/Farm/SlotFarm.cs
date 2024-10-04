using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private bool detectingWater;
    [SerializeField] private float waterAmount;

    private float currentWater;
    private bool dugHole;
    private bool plantedCarrot;
    private bool playerDetected;

    PlayerItemsController playerItemsController;

    private void Start()
    {
        playerItemsController = FindObjectOfType<PlayerItemsController>();
    }
    private void Update()
    {
        if (dugHole)
        {
            if (detectingWater)
            {
                currentWater += 0.01f;
            }

            if (currentWater >= waterAmount && !plantedCarrot)
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRenderer.sprite = carrot;
                plantedCarrot = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && plantedCarrot && playerDetected)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRenderer.sprite = hole;
                playerItemsController.CurrentCarrots++;
                currentWater = 0f;
                plantedCarrot = false;
            }
        }        
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= 0)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel") && digAmount > 0)
        {
            OnHit();
        }
        
        if (collision.CompareTag("Water"))
        {
            detectingWater = true;
        }

        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detectingWater = false;
        }

        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
