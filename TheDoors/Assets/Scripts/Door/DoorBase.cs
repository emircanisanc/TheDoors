using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class DoorBase : MonoBehaviour
{
    [SerializeField] protected Transform doorRotator;
    [SerializeField] float doorOpeningDuration = 1f;
    protected bool isOpen;

    protected virtual void Open()
    {
        isOpen = true;

        Vector3 doorAngle = new Vector3(0, -90, 0);
        doorRotator.DORotate(doorAngle, doorOpeningDuration);
    }
}
