using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableItemBase : InteractableBase
{
    [SerializeField] string itemName = "Item Name";
    [SerializeField] float infoTextSizeMultiplier = 0.5f;

    protected override float InfoSizeMultiplier()
    {
        return infoTextSizeMultiplier;
    }

    protected override string ItemName()
    {
        return itemName;
    }   
}
