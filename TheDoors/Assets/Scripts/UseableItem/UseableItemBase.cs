using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableItemBase : MonoBehaviour
{
    InventoryItem item;

    public virtual void Initialize(InventoryItem item)
    {
        this.item = item;
    }

    public virtual void UseItem(GameObject user)
    {
        Debug.Log(user.name + " used a " + item.itemSO.itemName);
    }
}
