using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painting : InteractableItemBase
{
    const string PAINTINGMESSAGE = "This painting called ";
    [SerializeField] string paintingName;

    protected override void OnInteract(GameObject interactor)
    {
        Message.ShowMessageInstance(PAINTINGMESSAGE + paintingName);
    }

}
