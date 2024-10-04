using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingArea : MonoBehaviour
{
    [SerializeField] private int chanceOfCatchingAFish;
    [SerializeField] private GameObject fishPrefab;

    private PlayerItemsController playerItemsController;
    private PlayerAnimation playerAnimation;
    private bool detectingPlayer;

    void Start()
    {
        playerItemsController = FindObjectOfType<PlayerItemsController>();
        playerAnimation = playerItemsController.GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnimation.OnCastingStarted();
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);
        if(randomValue < chanceOfCatchingAFish)
        {
            Instantiate(fishPrefab, playerItemsController.transform.position + new Vector3(Random.Range(-2f, -1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectingPlayer = false;
    }
}
