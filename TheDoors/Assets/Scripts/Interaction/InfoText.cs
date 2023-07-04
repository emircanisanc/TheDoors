using UnityEngine;
using TMPro;

/// <summary>
/// Handles displaying information text for interactable items.
/// </summary>
public class InfoText : MonoBehaviour
{
    public const string DEFAULT_INFO_STR = "ITEM NAME";

    static InfoText instance;

    [SerializeField] float minScale = 0.2f;
    [SerializeField] float maxScale = 1f;
    [SerializeField] float minDistance = 1f;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float distanceFactor = 0.7f;
    [SerializeField] TextMeshProUGUI tmpItemName;

    Transform cameraTransform;
    Transform objTransform;

    Canvas canvas;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        tmpItemName = GetComponentInChildren<TextMeshProUGUI>();

        cameraTransform = Camera.main.transform;

        canvas = GetComponent<Canvas>();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the information text visible for the provided transform with the given item name.
    /// </summary>
    /// <param name="transform">The transform of the interactable item.</param>
    /// <param name="itemName">The name of the item to display.</param>
    private void SetVisible(Transform transform, string itemName)
    {
        objTransform = transform;
        tmpItemName.SetText(itemName);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets the information text invisible.
    /// </summary>
    private void SetInvisible()
    {
        gameObject.SetActive(false);
    }

    private static void PrintErrorLog()
    {
        Debug.Log("THERE IS NO EXIST INFO CANVAS IN SCENE");
    }

    /// <summary>
    /// Sets the information text visible for the provided transform with the given item name.
    /// If there is no active instance of InfoText, an error log will be printed.
    /// </summary>
    /// <param name="transform">The transform of the interactable item.</param>
    /// <param name="itemName">The name of the item to display.</param>
    public static void SetVisibleInstance(Transform transform, string itemName)
    {
        if (instance)
            instance.SetVisible(transform, itemName);
        else
            PrintErrorLog();
    }

    /// <summary>
    /// Sets the information text invisible.
    /// If there is no active instance of InfoText, an error log will be printed.
    /// </summary>
    public static void SetInvisibleInstance()
    {
        if (instance)
            instance.SetInvisible();
        else
            PrintErrorLog();
    }

    void LateUpdate()
    {
        if (objTransform != null)
        {
            // Calculate the distance between the object and the camera
            float distance = Vector3.Distance(objTransform.position, cameraTransform.position);

            // Set the scale based on the distance
            float scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, distance));
            canvas.transform.localScale = Vector3.one * scale;

            // Face the object to the camera (in reverse)
            canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - cameraTransform.position);

            // Position the object in between the camera and the object
            Vector3 midPoint = (cameraTransform.position + objTransform.position) * 0.5f;
            Vector3 newPosition = midPoint + (objTransform.position - midPoint) * distanceFactor;
            canvas.transform.position = newPosition;
        }
    }
}
