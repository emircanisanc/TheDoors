using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public int currentStack; // The current stack or quantity of the item
    public ItemSO itemSO; // The ScriptableObject that represents the item
}
