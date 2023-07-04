using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour, IInteractable
{
    [SerializeField] [Min(1)] int amount = 1;

    public void SetAmount(int amount)
    {
        if (amount <= 0)
            return;
        this.amount = amount;
    }

    public void Interact(GameObject interactor)
    {
        //
    }

    public void StartHover(GameObject interactor)
    {
        if (interactor.TryGetComponent<Inventory>(out var inventory))
        {
            inventory.AddGold(amount);
            Destroy(gameObject);
        }
    }

    public Vector3 WorldPosition()
    {
        return transform.position;
    }

    public void EndHover(GameObject interactor)
    {
        //
    }
}
