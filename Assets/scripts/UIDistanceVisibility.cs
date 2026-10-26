using UnityEngine;

public class UIDistanceVisibility : MonoBehaviour
{
    public GameObject uiCanvas; 
    public float visibilityDistance = 5.0f; // Set the distance at which the UI becomes visible
    public Transform vrCamera;
    // Update is called once per frame
    void Update()
    {
   
        float distanceToUI = Vector3.Distance(uiCanvas.transform.position, vrCamera.transform.position);
        // Check if the distance is less than the visibility distance
        if (distanceToUI <= visibilityDistance)
        {
            // Show the UI if it's not already visible
            if (!uiCanvas.activeSelf)
            {
                uiCanvas.SetActive(true);
            }
        }
        else
        {
            // Hide the UI if it's currently visible
            if (uiCanvas.activeSelf)
            {
                uiCanvas.SetActive(false);
            }
        }
    }
}