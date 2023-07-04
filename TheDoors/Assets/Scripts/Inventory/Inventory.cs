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

    public Action<InventoryItem, int> OnItemAdded;
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

        int emptyAmount = InventorySlot - items.Count;
        for (int i = 0; i < emptyAmount; i++)
            items.Add(null);
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

    #region ITEM

    public bool GetItem(int slotIndex, out InventoryItem inventoryItem)
    {
        if (slotIndex >= InventorySlot || slotIndex < 0 || slotIndex >= items.Count)
        {
            inventoryItem = new InventoryItem();
            return false;
        }

        inventoryItem = items[slotIndex];

        return inventoryItem != null && inventoryItem.itemSO != null;

    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <returns>The remaining stack amount that could not be added to the inventory.</returns>
    public int AddItem(InventoryItem item)
    {
        if (item.itemSO.isStackable)
        {
            // Check if the item is stackable
            for (int i = 0; i < items.Count; i++)
            {
                InventoryItem inventoryItem = items[i];
                // Find an existing stack with the same item
                if (inventoryItem.itemSO != null && inventoryItem.itemSO.itemName == item.itemSO.itemName)
                {
                    int amountToAdd = item.currentStack;
                    int currentStack = inventoryItem.currentStack;
                    int maxStack = item.itemSO.maxStack;

                    int spaceLeftInStack = maxStack - currentStack;
                    // Calculate the amount that can be added to the existing stack
                    int amountAdded = Mathf.Min(amountToAdd, spaceLeftInStack);
                    inventoryItem.currentStack += amountAdded;
                    item.currentStack -= amountAdded;

                    OnItemAdded?.Invoke(inventoryItem, i);

                    if (item.currentStack <= 0)
                    {
                        // Item fully added to an existing stack
                        return 0;
                    }
                }
            }

            if (item.currentStack > 0)
            {
                if (items.Count < inventorySlot)
                {
                    AddNewStack(item);
                    return 0;
                }
                else
                {
                    // Find an empty slot and add the item
                    int emptySlotIndex = FindEmptySlot();
                    if (emptySlotIndex != -1)
                    {
                        items[emptySlotIndex] = item;
                        OnItemAdded?.Invoke(item, emptySlotIndex);
                        return 0;
                    }
                }
            }

            // Inventory is full or no empty slot available, return remaining stack amount
            return item.currentStack;
        }
        else
        {
            if (items.Count < inventorySlot)
            {
                AddNewStack(item);
                return 0;
            }
            else
            {
                // Find an empty slot and add the item
                int emptySlotIndex = FindEmptySlot();
                if (emptySlotIndex != -1)
                {
                    items[emptySlotIndex] = item;
                    OnItemAdded?.Invoke(item, emptySlotIndex);
                    return 0;
                }
            }

            // Inventory is full or no empty slot available, return the current stack amount
            return item.currentStack;
        }
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                return i;
            if (items[i].itemSO == null)
                return i;
        }
        return -1; // No empty slot found
    }

    private void AddNewStack(InventoryItem item)
    {
        items.Add(item);
        OnItemAdded?.Invoke(items[items.Count - 1], items.Count - 1);
    }


    #endregion
}
