using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    [Header("Current Values")]
    [SerializeField] private float currentWater;
    [SerializeField] private float currentWood;
    [SerializeField] private int currentCarrots;

    [Header("Limit Values")]
    [SerializeField] private float waterLimit;
    [SerializeField] private float woodLimit;
    [SerializeField] private float carrotLimit;

    #region Properties
    public float CurrentWood { get => currentWood; set => currentWood = value; }
    public float CurrentWater { get => currentWater; set => currentWater = value; }
    public int CurrentCarrots { get => currentCarrots; set => currentCarrots = value; }
    public float WaterLimit { get => waterLimit; set => waterLimit = value; }
    public float WoodLimit { get => woodLimit; set => woodLimit = value; }
    public float CarrotLimit { get => carrotLimit; set => carrotLimit = value; }
    #endregion

    public void WaterLimitation(float waterValue)
    {
        if(currentWater < WaterLimit)
        {
            currentWater += waterValue;
        }
    }
}
