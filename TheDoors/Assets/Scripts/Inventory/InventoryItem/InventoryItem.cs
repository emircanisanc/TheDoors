using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventoryItem
{
    public Action <InventoryItem> OnItemDestroyed;
    public Action <float> OnItemFuelChanged;

    private float currentFuel = 100; // The current charge or fuel of the item
    public float CurrentFuel {
         get { return currentFuel; }
     set { currentFuel = value; OnItemFuelChanged?.Invoke(currentFuel); } }
    public ItemSO itemSO; // The ScriptableObject that represents the item
}
