using UnityEngine;
using UnityEngine.InputSystem;
public class Interactor : MonoBehaviour
{
    [SerializeField] LayerMask interactLayer;
    [SerializeField] float hoverDistance = 20;
    [SerializeField] float interactDistance = 10;

    InteractionInput interaction;

    Transform mainCameraTransform;
    InteractableBase lastInteracted;

    void Awake()
    {
        mainCameraTransform = Camera.main.transform;
        interaction = new InteractionInput();
        interaction.Interaction.Interact.performed += _ => Interact();
    }

    void OnEnable()
    {
        interaction.Interaction.Enable();
    }
    void OnDisable()
    {
        interaction.Interaction.Disable();
    }

    void LateUpdate()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out raycastHit, hoverDistance, interactLayer))
        {
            if (raycastHit.transform.TryGetComponent<InteractableBase>(out var interactable))
            {
                // Check if a new interactable object is detected
                if (interactable != lastInteracted)
                {
                    FinishLastInteraction();
                    lastInteracted = interactable;
                    interactable.StartHover(gameObject);
                }
            }
            else
            {
                // No interactable object detected, finish last interaction
                FinishLastInteraction();
            }
        }
        else
        {
            // No interactable object detected, finish last interaction
            FinishLastInteraction();
        }
    }

    public void Interact()
    {
        if (lastInteracted == null)
            return;

        // Check if the player is within interact distance
        if (Vector3.Distance(transform.position, lastInteracted.WorldPosition()) <= interactDistance)
        {
            // End hover and perform interaction
            InteractableBase objToInteract = lastInteracted;
            lastInteracted = null;
            objToInteract.Interact(gameObject);
        }
    }

    private void FinishLastInteraction()
    {
        if (lastInteracted != null)
        {
            // Finish the last interaction by ending hover
            lastInteracted.EndHover(gameObject);
            lastInteracted = null;
        }
    }
}
