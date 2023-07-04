using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] GameObject hoverImage;
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemStackText;

    /// <summary>
    /// Opens the hover image.
    /// </summary>
    public void OpenHover()
    {
        hoverImage.SetActive(true);
    }

    /// <summary>
    /// Closes the hover image.
    /// </summary>
    public void CloseHover()
    {
        hoverImage.SetActive(false);
    }

    /// <summary>
    /// Sets the icon sprite of the item.
    /// </summary>
    /// <param name="iconSprite">The sprite to set as the icon.</param>
    public void SetIcon(Sprite iconSprite)
    {
        itemIcon.sprite = iconSprite;
    }

    /// <summary>
    /// Sets the stack amount of the item.
    /// </summary>
    /// <param name="amount">The stack amount to set.</param>
    public void SetStackAmount(int amount)
    {
        if (amount == 0 || amount == 1)
        {
            itemStackText.gameObject.SetActive(false);
        }
        else
        {
            itemStackText.gameObject.SetActive(true);
            itemStackText.text = amount.ToString();
        }
    }
}
