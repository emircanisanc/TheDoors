using UnityEngine;

/// <summary>
/// Abstract base class for item scriptable objects.
/// </summary>
[CreateAssetMenu(menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    /// <summary>
    /// The name of the item.
    /// </summary>
    public string itemName;

    /// <summary>
    /// Indicates whether the item is consumeable.
    /// </summary>
    public bool isConsumeable;

    public float fuelSpendMultiplier;

    /// <summary>
    /// The inventory icon sprite for the item.
    /// </summary>
    public Sprite inventoryIcon;

    /// <summary>
    /// The prefab associated with the item in the world representation.
    /// </summary>
    public GameObject pfWorld;

    /// <summary>
    /// The prefab associated with the item on the hand representation.
    /// </summary>
    public GameObject pfOnHand;

    public float infoTextSizeMultiplier = 1;

}
