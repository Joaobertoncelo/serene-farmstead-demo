using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    [Header("Current Values")]
    [SerializeField] private float currentWood;
    [SerializeField] private float currentWater;

    #region Properties
    public float CurrentWood { get => currentWood; set => currentWood = value; }
    public float CurrentWater { get => currentWater; set => currentWater = value; }
    #endregion

    [Header("Limit Values")]
    [SerializeField] private float waterLimit;

    public void WaterLimit(float waterValue)
    {
        if(currentWater < waterLimit)
        {
            currentWater += waterValue;
        }
    }
}
