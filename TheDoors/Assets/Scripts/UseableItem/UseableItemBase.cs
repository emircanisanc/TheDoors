using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableItemBase : MonoBehaviour
{
    protected bool isActive;
    protected InventoryItem itemData;

    public void ToggleActive()
    {
        if (isActive)
            SetDeactive();
        else
            SetActive();
    }

    public virtual void SetActive()
    {
        isActive = true;
    }

    public virtual void SetDeactive()
    {
        isActive = false;
    }

    public void InitializeItem(InventoryItem itemData)
    {
        this.itemData = itemData;
    }

    protected virtual void Update()
    {
        if (!isActive)
            return;

        Debug.Log("Item is working!");
    }
}
