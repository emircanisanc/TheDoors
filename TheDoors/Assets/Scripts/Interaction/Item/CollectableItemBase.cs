using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemBase : InteractableBase
{
    [SerializeField] protected InventoryItem itemData;

    public InventoryItem ItemData => itemData;

    protected override float InfoSizeMultiplier()
    {
        return  itemData.itemSO.infoTextSizeMultiplier;
    }

    protected override void OnInteract(GameObject interactor)
    {
        if (interactor.TryGetComponent<Inventory>(out var inventory))
        {
            if (inventory.AddItem(itemData))
                Destroy(gameObject);
        }
    }

    protected override string ItemName()
    {
        return ItemData.itemSO.itemName;
    }
}
