using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlashlightWorld : CollectableItemBase
{
    protected override void OnInteract(GameObject interactor)
    {
        if (interactor.TryGetComponent<Inventory>(out var inventory))
        {
            var flashlights = inventory.FindItems("Flashlight");
            if (flashlights.Count > 0)
            {
                var lowestFlashlight = flashlights.OrderBy(x => x.CurrentFuel).FirstOrDefault();
                if (lowestFlashlight != null)
                {
                    lowestFlashlight.CurrentFuel = 100f;
                    Message.ShowMessageInstance("Flashlights battery added");
                    Destroy(gameObject);
                }
            }
            else
            {
                if (inventory.AddItem(itemData))
                    Destroy(gameObject);
            }
        }
    }
}
