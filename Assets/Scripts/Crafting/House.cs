using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private int necessaryWood;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private GameObject houseCollider;
    [SerializeField] private SpriteRenderer roofSprite;
    [SerializeField] private SpriteRenderer doorSprite;
    [SerializeField] private Transform playerPoint;
    

    private bool detectingPlayer;
    private Player player;
    private PlayerAnimation playerAnimation;
    private PlayerItemsController playerItemsController;

    private float timeCount;
    private bool constructing;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnimation = player.GetComponent<PlayerAnimation>();
        playerItemsController = player.GetComponent<PlayerItemsController>();
    }

    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItemsController.CurrentWood >= necessaryWood)
        {
            constructing = true;
            playerAnimation.OnHammeringStarted();
            roofSprite.color = startColor;
            doorSprite.color = startColor;
            player.transform.position = playerPoint.position;
            player.isPaused = true;
            playerItemsController.CurrentWood -= necessaryWood;        }

        if (constructing)
        {
            timeCount += Time.deltaTime;

            if(timeCount > timeAmount)
            {
                playerAnimation.OnHammeringEnded();
                roofSprite.color = endColor;
                doorSprite.color = endColor;
                player.isPaused = false;
                houseCollider.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
