using UnityEngine;

/// <summary>
/// Interface for interactable objects.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Perform the interaction with the interactor.
    /// </summary>
    /// <param name="interactor">The GameObject interacting with this object.</param>
    void Interact(GameObject interactor);

    /// <summary>
    /// Start the hover effect for the interactor.
    /// </summary>
    /// <param name="interactor">The GameObject hovering over this object.</param>
    void StartHover(GameObject interactor);

    /// <summary>
    /// End the hover effect for the interactor.
    /// </summary>
    /// <param name="interactor">The GameObject that stopped hovering over this object.</param>
    void EndHover(GameObject interactor);

    /// <summary>
    /// Get the world position of this object.
    /// </summary>
    /// <returns>The world position of this object.</returns>
    Vector3 WorldPosition();
}
