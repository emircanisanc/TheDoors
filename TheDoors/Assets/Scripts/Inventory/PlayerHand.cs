using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform itemAttachSocket;
    
    public Action <int> OnActiveItemChanged;

    UseableItemBase itemOnHand;
    int currentItemIndex;

    void Awake()
    {
        itemOnHand = null;

        inventory.OnItemAdded += CheckOnHandItem;
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0)
            ChangeActiveItemSlot(currentItemIndex + 1);
        else if (scrollInput < 0)
            ChangeActiveItemSlot(currentItemIndex - 1);

        if (Input.GetMouseButtonDown(0))
        {
            UseCurrentlyHeldObject();
        }
    }

    private void UseCurrentlyHeldObject()
    {
        if (itemOnHand == null)
            return;

        itemOnHand.UseItem(gameObject);
    }

    private void CheckOnHandItem(InventoryItem item, int itemSlot)
    {
        if (itemOnHand)
            return;

        if (currentItemIndex != itemSlot)
            return;
        
        SpawnItemAndUseIt(item);

    }

    private void ChangeActiveItemSlot(int itemSlot)
    {
        if (itemSlot >= inventory.InventorySlot)
            currentItemIndex = 0;
        else if (itemSlot < 0)
            currentItemIndex = inventory.InventorySlot - 1;
        else
            currentItemIndex = itemSlot;

        OnActiveItemChanged?.Invoke(currentItemIndex);

        if (itemOnHand)
        {
            Destroy(itemOnHand.gameObject);
            TryUseItemAt(currentItemIndex);
        }
        else
        {
            TryUseItemAt(currentItemIndex);
        }
    }

    private void TryUseItemAt(int itemSlot)
    {
        InventoryItem inventoryItem;
        if (inventory.GetItem(itemSlot, out inventoryItem))
        {
            currentItemIndex = itemSlot;
            SpawnItemAndUseIt(inventoryItem);
            return;
        }
        itemOnHand = null;
    }

    private void SpawnItemAndUseIt(InventoryItem item)
    {
        GameObject itemOnHandPrefab = Instantiate(item.itemSO.pfOnHand, itemAttachSocket);
        itemOnHand = itemOnHandPrefab.GetComponent<UseableItemBase>();

        itemOnHand.Initialize(item);
    }
    
}
