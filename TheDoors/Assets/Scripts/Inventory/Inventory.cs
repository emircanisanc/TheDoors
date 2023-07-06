using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Represents the player's inventory.
/// </summary>
public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventoryItem> items;
    [SerializeField] int inventorySlot = 3;
    [SerializeField][Min(0)] int currentGold = 0;

    public Action<List<InventoryItem>> OnInventoryChanged;
    public Action<int> OnGoldAmountChanged;

    /// <summary>
    /// The number of available inventory slots.
    /// </summary>
    public int InventorySlot => inventorySlot;

    /// <summary>
    /// The current amount of gold.
    /// </summary>
    public int CurrentGold => currentGold;

    void Awake()
    {
        if (items == null)
            items = new List<InventoryItem>();
    }

    #region GOLD

    /// <summary>
    /// Adds the specified amount of gold to the inventory.
    /// </summary>
    /// <param name="amount">The amount of gold to add.</param>
    public void AddGold(int amount)
    {
        currentGold = Mathf.Max(currentGold, currentGold + amount);
        OnGoldAmountChanged?.Invoke(currentGold);
    }

    /// <summary>
    /// Reduces the current gold amount by the specified amount.
    /// </summary>
    /// <param name="amount">The amount of gold to reduce.</param>
    /// <returns><c>true</c> if the gold was successfully reduced, <c>false</c> if the current gold is not enough.</returns>
    public bool ReduceGold(int amount)
    {
        if (currentGold < amount)
            return false;

        currentGold -= amount;
        OnGoldAmountChanged?.Invoke(currentGold);
        return true;
    }

    /// <summary>
    /// Checks if the current gold amount is sufficient for the specified amount.
    /// </summary>
    /// <param name="amount">The amount to compare with the current gold.</param>
    /// <returns><c>true</c> if the current gold is greater than or equal to the specified amount, <c>false</c> otherwise.</returns>
    public bool IsGoldEnough(int amount)
    {
        return currentGold >= amount;
    }

    #endregion


    # region ITEM

    public bool AddItem(InventoryItem item)
    {
        if (items.Count >= InventorySlot)
        {
            // DISPLAY ERROR MESSAGE
            return false;
        }

        items.Add(item);
        item.OnItemDestroyed += RemoveItem;
        OnInventoryChanged?.Invoke(items);
        return true;
    }

    public void RemoveItem(InventoryItem item)
    {
        int index = items.FindIndex(x => x == item);
        items.RemoveAt(index);

        OnInventoryChanged?.Invoke(items);
    }

    public bool TryGetItem(int index, out InventoryItem itemData)
    {
        itemData = null;

        if (index >= items.Count)
            return false;

        itemData = items[index];
        return true;
    }

    # endregion

}
