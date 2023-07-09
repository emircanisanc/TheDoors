using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : InteractableItemBase
{
    [SerializeField] [Min(1)] int goldAmount = 1;

    public override void StartHover(GameObject interactor)
    {
        if (!isActive)
            return;

        OutlineActive();

        if (interactor.TryGetComponent<Inventory>(out var inventory))
        {
            inventory.AddGold(goldAmount);
            
            isActive = false;
            Invoke(nameof(OnCollectGold), 0.5f);
            Destroy(gameObject, 1f);
        }
    }

    private void OnCollectGold()
    {
        OutlinePassive();
    }

    public override void EndHover(GameObject interactor)
    {
        OutlinePassive();
    }


    protected override void OnInteract(GameObject interactor)
    {
        return;
    }

}
