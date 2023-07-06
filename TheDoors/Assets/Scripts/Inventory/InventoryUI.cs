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
    int lastHoverSlot = -1;

    void Awake() 
    {
        itemSlots = new List<ItemSlotUI>();

        inventorySlotTemp.SetActive(false);
    }

    void Start()
    {
        CreateSlots(inventory.InventorySlot);
        UpdateGoldAmount(inventory.CurrentGold);

        inventory.OnGoldAmountChanged += UpdateGoldAmount;
        inventory.OnInventoryChanged += UpdateSlots;

        playerHand.OnActiveItemChanged += UpdateHoverItem;    
    }

    private void CreateSlots(int slotAmount)
    {

        for (int i = 0; i < slotAmount; i ++)
        {
            GameObject slot = Instantiate(inventorySlotTemp, inventorySlotTemp.transform.parent);
            itemSlots.Add(slot.GetComponent<ItemSlotUI>());
        }

        Vector2 lastSize = inventoryBackgroundRectTransform.sizeDelta;
        float x = itemSlots.Count * inventorySlotX + (itemSlots.Count + 1) * spaceBetweenSlots;
        inventoryBackgroundRectTransform.sizeDelta = new Vector2(x + 20, lastSize.y);
    }

    private void UpdateSlots(List<InventoryItem> items)
    {
        for (int i = 0; i < itemSlots.Count; i ++)
        {
            itemSlots[i].SetSlotIndex(i + 1);
            
            if (items.Count > i)
                itemSlots[i].InitSlotItem(items[i]);
            else
                itemSlots[i].InitSlotItem(null);

        }

        Vector2 lastSize = inventoryBackgroundRectTransform.sizeDelta;
        float x = items.Count * inventorySlotX + (itemSlots.Count + 1) * spaceBetweenSlots;
        inventoryBackgroundRectTransform.sizeDelta = new Vector2(x + 20, lastSize.y);
    }

    private void UpdateHoverItem(int index)
    {
        if (index != lastHoverSlot)
        {
            if (itemSlots.Count > lastHoverSlot && lastHoverSlot >= 0)
                itemSlots[lastHoverSlot].CloseHover();

            if (itemSlots.Count > index && index >= 0)
                itemSlots[index].OpenHover();
        }
        else
        {
            if (itemSlots.Count > lastHoverSlot && lastHoverSlot >= 0)
                itemSlots[lastHoverSlot].ToggleHover();
        }

        lastHoverSlot = index;
        
    }

    private void UpdateGoldAmount(int goldAmount)
    {
        tmpGold.SetText(goldAmount.ToString()); // Update the text displaying the gold amount
    }

}
