using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] LayerMask interactLayer;
    [SerializeField] float hoverDistance = 20;
    [SerializeField] float interactDistance = 10;

    Transform mainCameraTransform;
    IInteractable lastInteracted;

    void Awake()
    {
        mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out raycastHit, hoverDistance, interactLayer))
        {
            if (raycastHit.transform.TryGetComponent<IInteractable>(out var interactable))
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lastInteracted == null)
                return;

            // Check if the player is within interact distance
            if (Vector3.Distance(transform.position, lastInteracted.WorldPosition()) <= interactDistance)
            {
                // End hover and perform interaction
                lastInteracted.EndHover(gameObject);
                lastInteracted.Interact(gameObject);
                lastInteracted = null;
            }
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
