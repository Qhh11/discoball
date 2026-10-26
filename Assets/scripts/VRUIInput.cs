using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRUIInput : MonoBehaviour
{
    public Transform controllerTransform;
    public OVRInput.Button selectButton = OVRInput.Button.One;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>(); // Find the Event System in the scene
        pointerEventData = new PointerEventData(eventSystem);
        raycastResults = new List<RaycastResult>();
    }

    void Update()
    {
        // Create a ray from the controller
        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
        pointerEventData.position = new Vector2(ray.origin.x, ray.origin.y); // Set a fake position

        // Raycast using the Graphics Raycaster and pointer event data
        raycastResults.Clear();
        eventSystem.RaycastAll(pointerEventData, raycastResults);

        // Go through all the raycast results
        foreach (RaycastResult result in raycastResults)
        {
            // Check for UI hit
            GameObject hitObject = result.gameObject;
            Debug.Log("Hit " + hitObject.name);

            // Check if the object hit is a UI element
            if (hitObject.GetComponent<RectTransform>() != null) // UI components will have a RectTransform
            {
                // Check if the select button is pressed
                if (OVRInput.GetDown(selectButton))
                {
                    ExecuteEvents.Execute(hitObject, pointerEventData, ExecuteEvents.submitHandler);
                }
                break; // Only handle the topmost UI element
            }
        }
    }
}
