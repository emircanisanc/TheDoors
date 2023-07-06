using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] GameObject hoverImageClosed;
    [SerializeField] GameObject hoverImageOpened;
    [SerializeField] GameObject fuelBar;
    [SerializeField] Image barFill;
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemSlotIndex;

    InventoryItem itemInSlot;

    public InventoryItem ItemInSlot { get { return itemInSlot; } set { itemInSlot = value; } }

    /// <summary>
    /// Opens the hover image.
    /// </summary>
    public void OpenHover()
    {
        hoverImageClosed.SetActive(false);
        hoverImageOpened.SetActive(true);
    }

    /// <summary>
    /// Closes the hover image.
    /// </summary>
    public void CloseHover()
    {
        hoverImageClosed.SetActive(true);
        hoverImageOpened.SetActive(false);
    }

    public void ToggleHover()
    {
        if (hoverImageClosed.activeSelf)
            OpenHover();
        else
            CloseHover();
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
    /// Sets the slot index of the item.
    /// </summary>
    /// <param name="index">The stack amount to set.</param>
    public void SetSlotIndex(int index)
    {
        itemSlotIndex.SetText(index.ToString());
    }

    public void InitSlotItem(InventoryItem item)
    {
        if (itemInSlot != null)
        {
            if (itemInSlot.itemSO.isConsumeable)
            {
                itemInSlot.OnItemFuelChanged -= UpdateFuelAmount;
            }
        }

        if (item == null || item.itemSO == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            itemInSlot = item;
            if (itemInSlot.itemSO.isConsumeable)
            {
                fuelBar.SetActive(true);
                UpdateFuelAmount(itemInSlot.CurrentFuel);
                itemInSlot.OnItemFuelChanged += UpdateFuelAmount;
            }
            else
            {
                fuelBar.SetActive(false);
            }
            SetIcon(itemInSlot.itemSO.inventoryIcon);
        }
    }

    private void UpdateFuelAmount(float amount)
    {
        barFill.fillAmount = amount / 100f;
    }
}
