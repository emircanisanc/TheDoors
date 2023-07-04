using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for items in the game.
/// </summary>
public abstract class ItemBase : InteractableItemBase
{
    [SerializeField] InventoryItem itemData;

    /// <summary>
    /// Called when the item starts being hovered by an interactor.
    /// </summary>
    /// <param name="interactor">The GameObject of the interactor.</param>
    public override void StartHover(GameObject interactor)
    {
        OutlineActive();
        InfoText.SetVisibleInstance(transform, itemData.itemSO.itemName);
    }

    /// <summary>
    /// Gets the ScriptableObject data for the item.
    /// </summary>
    /// <returns>The ItemSO data for the item.</returns>
    public abstract ItemSO GetItemSO();

    /// <summary>
    /// Called when the item is interacted with by an interactor.
    /// </summary>
    /// <param name="interactor">The GameObject of the interactor.</param>
    public override void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent<Inventory>(out var inventory))
        {
            int stackAmount = inventory.AddItem(itemData);
            if (stackAmount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
}
