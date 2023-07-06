using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventoryItem
{
    public Action <InventoryItem> OnItemDestroyed;
    public Action <float> OnItemFuelChanged;

    public float currentFuel; // The current charge or fuel of the item
    public ItemSO itemSO; // The ScriptableObject that represents the item
}
