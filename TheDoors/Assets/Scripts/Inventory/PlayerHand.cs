using UnityEngine;
using System;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform itemAttachSocket;
    
    public Action <int> OnActiveItemChanged;

    float nextUseItemTime;
    int currentItemIndex = -1;
    UseableItemBase itemOnHand;

    void Awake()
    {
        itemOnHand = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeItem(3);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextUseItemTime)
            {
                nextUseItemTime = Time.time + 0.3f;
                if (itemOnHand != null)
                {
                    itemOnHand.ToggleActive();
                }
            }
            
        }
    }

    public void ChangeItem(int index)
    {
        InventoryItem itemData;

        if (currentItemIndex == index)
        {
            if (itemOnHand == null)
            {
                if (inventory.TryGetItem(index, out itemData))
                {
                    GameObject obj = Instantiate(itemData.itemSO.pfOnHand, itemAttachSocket);
                    itemOnHand = obj.GetComponent<UseableItemBase>();
                    itemOnHand.InitializeItem(itemData);
                    currentItemIndex = index;
                }
                else
                {
                    currentItemIndex = -1;
                }
                OnActiveItemChanged?.Invoke(currentItemIndex);
            }
            else
            {
                Destroy(itemOnHand.gameObject);
                currentItemIndex = -1;
                OnActiveItemChanged?.Invoke(currentItemIndex);
            }
            return;
        }

        if (itemOnHand != null)
        {
            Destroy(itemOnHand.gameObject);
        }

        if (inventory.TryGetItem(index, out itemData))
        {
            GameObject obj = Instantiate(itemData.itemSO.pfOnHand, itemAttachSocket);
            itemOnHand = obj.GetComponent<UseableItemBase>();
            itemOnHand.InitializeItem(itemData);
            currentItemIndex = index;
        }
        else
        {
            currentItemIndex = -1;
        }
        OnActiveItemChanged?.Invoke(currentItemIndex);
    }
    
}
