using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Message : MonoBehaviour
{
    static Message instance;


    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI tmpMessage;


    string lastString;
    bool isMessageActive;
    Sequence sequence;


    private void ShowMessage(string message)
    {
        if (isMessageActive)
        {

        }
        else
        {
            tmpMessage.SetText(message);
            sequence = DOTween.Sequence();
            sequence.Append(canvasGroup.DOFade(1, 0.5f));
        }
    }


    public static void ShowMessageInstance(string message)
    {
        if (instance)
            instance.ShowMessage(message);
        else
            Debug.Log("There is no instance of Message Class");
    }
}
