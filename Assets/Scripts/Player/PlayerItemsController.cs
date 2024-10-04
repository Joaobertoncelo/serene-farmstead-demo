using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    [Header("Current Values")]
    [SerializeField] private float currentWater;
    [SerializeField] private float currentWood;
    [SerializeField] private float currentCarrots;
    [SerializeField] private float currentFishes;

    [Header("Limit Values")]
    [SerializeField] private float waterLimit;
    [SerializeField] private float woodLimit;
    [SerializeField] private float carrotLimit;
    [SerializeField] private float fishesLimit;

    #region Properties
    public float CurrentWood { get => currentWood; set => currentWood = value; }
    public float CurrentWater { get => currentWater; set => currentWater = value; }
    public float CurrentCarrots { get => currentCarrots; set => currentCarrots = value; }
    public float WaterLimit { get => waterLimit; set => waterLimit = value; }
    public float WoodLimit { get => woodLimit; set => woodLimit = value; }
    public float CarrotLimit { get => carrotLimit; set => carrotLimit = value; }
    public float CurrentFishes { get => currentFishes; set => currentFishes = value; }
    public float FishesLimit { get => fishesLimit; set => fishesLimit = value; }
    #endregion

    public void WaterLimitation(float waterValue)
    {
        if(currentWater < WaterLimit)
        {
            currentWater += waterValue;
        }
    }
}
