using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : DoorBase
{

    void OnTriggerEnter(Collider other)
    {
        if (isOpen)
            return;

        if (!other.CompareTag("Player"))
            return;

        Open();
    }
}
