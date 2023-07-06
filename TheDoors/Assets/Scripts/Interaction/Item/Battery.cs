using UnityEngine;
using System.Linq;

public class Battery : InteractableItemBase
{
    [SerializeField] float addFuelAmount = 50;

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
                    lowestFlashlight.CurrentFuel = Mathf.Min(100, lowestFlashlight.CurrentFuel + addFuelAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}
