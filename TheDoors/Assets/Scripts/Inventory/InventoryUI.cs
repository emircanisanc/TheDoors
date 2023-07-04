using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] PlayerHand playerHand;

    [Header("Items")]
    [SerializeField] RectTransform inventoryBackgroundRectTransform;
    [SerializeField] GameObject inventorySlotTemp;
    [SerializeField] float inventorySlotX;
    [SerializeField] float spaceBetweenSlots;
    
    [Header("Gold")]
    [SerializeField] TextMeshProUGUI tmpGold;

    List<ItemSlotUI> itemSlots;
    int lastHoverSlot;

    void Awake() 
    {
        itemSlots = new List<ItemSlotUI>();
        lastHoverSlot = 0;

        inventorySlotTemp.SetActive(false);

        UpdateSlotAmount(inventory.InventorySlot);
        UpdateGoldAmount(inventory.CurrentGold);
        UpdateHoverItem(0);

        inventory.OnItemAdded += UpdateSlot;
        inventory.OnGoldAmountChanged += UpdateGoldAmount;
        playerHand.OnActiveItemChanged += UpdateHoverItem;
    }

    private void UpdateSlotAmount(int slotAmount)
    {
        ClearSlots();

        for (int i = 0; i < slotAmount; i ++)
        {
            GameObject slot = Instantiate(inventorySlotTemp, inventorySlotTemp.transform.parent);
            slot.SetActive(true);
            itemSlots.Add(slot.GetComponent<ItemSlotUI>());
        }

        Vector2 lastSize = inventoryBackgroundRectTransform.sizeDelta;
        float x = itemSlots.Count * inventorySlotX + (itemSlots.Count + 1) * spaceBetweenSlots;
        inventoryBackgroundRectTransform.sizeDelta = new Vector2(x, lastSize.y);
    }

    private void ClearSlots()
    {
        if (itemSlots == null)
            return;

        for (int i = 0; i < itemSlots.Count; i++)
        {
            Destroy(itemSlots[i].gameObject);
        }

        itemSlots.Clear();
    }

    private void UpdateSlot(InventoryItem item, int index)
    {
        ItemSlotUI itemSlot = itemSlots[index];
        if (item == null)
        {
            itemSlot.SetIcon(null); // Clear the icon if item is null
            itemSlot.SetStackAmount(0); // Clear the stack amount if item is null
            return;
        }

        if (item.itemSO == null)
        {
            itemSlot.SetIcon(null); // Clear the icon if item is null
            itemSlot.SetStackAmount(0); // Clear the stack amount if item is null
            return;
        }   
        itemSlot.SetIcon(item.itemSO.inventoryIcon); // Set the icon of the item
        itemSlot.SetStackAmount(item.currentStack); // Set the stack amount of the item
    }

    private void UpdateGoldAmount(int goldAmount)
    {
        tmpGold.SetText(goldAmount.ToString()); // Update the text displaying the gold amount
    }

    private void UpdateHoverItem(int hoverSlot)
    {
        itemSlots[lastHoverSlot].CloseHover();
        
        lastHoverSlot = hoverSlot;
        itemSlots[lastHoverSlot].OpenHover();
    }
}
