﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherWater : MonoBehaviour
{
    [SerializeField] private bool detectingPlayer;
    [SerializeField] private int waterValue;

    private PlayerItemsController playerItemsController;

    void Start()
    {
        playerItemsController = FindObjectOfType<PlayerItemsController>();
    }

    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerItemsController.WaterLimit(waterValue);
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
