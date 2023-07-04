using UnityEngine;

/// <summary>
/// Base class for interactable items.
/// </summary>
public abstract class InteractableItemBase : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject outline;

    /// <summary>
    /// Start the hover effect for the interactor.
    /// </summary>
    /// <param name="interactor">The GameObject hovering over this item.</param>
    public virtual void StartHover(GameObject interactor)
    {
        OutlineActive();
        InfoText.SetVisibleInstance(transform, InfoText.DEFAULT_INFO_STR);
    }

    /// <summary>
    /// End the hover effect for the interactor.
    /// </summary>
    /// <param name="interactor">The GameObject that stopped hovering over this item.</param>
    public virtual void EndHover(GameObject interactor)
    {
        OutlinePassive();
        InfoText.SetInvisibleInstance();
    }

    /// <summary>
    /// Get the world position of this item.
    /// </summary>
    /// <returns>The world position of this item.</returns>
    public Vector3 WorldPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// Perform the interaction with the interactor.
    /// </summary>
    /// <param name="interactor">The GameObject interacting with this item.</param>
    public abstract void Interact(GameObject interactor);

    /// <summary>
    /// Activate the outline for this item.
    /// </summary>
    protected void OutlineActive()
    {
        outline.SetActive(true);
    }

    /// <summary>
    /// Deactivate the outline for this item.
    /// </summary>
    protected void OutlinePassive()
    {
        outline.SetActive(false);
    }
}
