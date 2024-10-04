using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Itens")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;
    [SerializeField] private Image fishUIBar;

    [Header("Tools")]
    public List<Image> toolsUI = new List<Image>();

    [SerializeField] private Color selectedToolColor;
    [SerializeField] private Color notSelectedToolColor;

    private PlayerItemsController playerItemsController;
    private Player player;

    private void Awake()
    {
        playerItemsController = FindObjectOfType<PlayerItemsController>();
        player = playerItemsController.GetComponent<Player>();
    }

    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        carrotUIBar.fillAmount = 0f;
        fishUIBar.fillAmount = 0f;
    }

    void Update()
    {
        waterUIBar.fillAmount = playerItemsController.CurrentWater / playerItemsController.WaterLimit;
        woodUIBar.fillAmount = playerItemsController.CurrentWood / playerItemsController.WoodLimit;
        carrotUIBar.fillAmount = playerItemsController.CurrentCarrots / playerItemsController.CarrotLimit;
        fishUIBar.fillAmount = playerItemsController.CurrentFishes / playerItemsController.FishesLimit;

        toolsUI[player.HandlingObject].color = selectedToolColor;

        for (int i=0; i < toolsUI.Count; i++)
        {
            if (i == player.HandlingObject)
            {
                toolsUI[i].color = selectedToolColor;
            }
            else
            {
                toolsUI[i].color = notSelectedToolColor;
            }
        }
    }
}
