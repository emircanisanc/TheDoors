using UnityEngine;

/// <summary>
/// Base class for interactables.
/// </summary>
public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] outlines;
    [SerializeField] protected bool isActive = true;


    protected abstract string ItemName();

    /// <summary>
    /// Start the hover effect for the interactor.
    /// </summary>
    /// <param name="interactor">The GameObject hovering over this item.</param>
    public virtual void StartHover(GameObject interactor)
    {
        if (!isActive)
            return;

        OutlineActive();
        InfoText.SetVisibleInstance(transform, ItemName(), 1);
    }

    /// <summary>
    /// End the hover effect for the interactor.
    /// </summary>
    /// <param name="interactor">The GameObject that stopped hovering over this item.</param>
    public virtual void EndHover(GameObject interactor)
    {
        if (!isActive)
            return;

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
    public void Interact(GameObject interactor)
    {
        if (!isActive)
            return;

        EndHover(interactor);
        OnInteract(interactor);
    }

    protected abstract void OnInteract(GameObject interactor);

    /// <summary>
    /// Activate the outline for this item.
    /// </summary>
    protected void OutlineActive()
    {
        foreach (var outline in outlines)
            outline.SetActive(true);
    }

    /// <summary>
    /// Deactivate the outline for this item.
    /// </summary>
    protected void OutlinePassive()
    {
        foreach (var outline in outlines)
            outline.SetActive(false);
    }
}
