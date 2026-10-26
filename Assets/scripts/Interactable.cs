using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    [SerializeField]
    private Transform interactablePosition;
    [SerializeField]
    private float distanceFromInteractable = 5f;
    [SerializeField]
    private float visibleUntil = 5f;
    private GameObject hintInstantiated;

    public UnityEvent OnHover;
    public UnityEvent OnInteract;

    public void ShowInteractionHint(GameObject prefab, Camera origin) {
        var originTransform = origin.transform;
        var interactablePositionPosition = interactablePosition.position;
        var originTransformPosition = originTransform.position;
        if (Vector3.Distance(interactablePositionPosition, originTransformPosition) <= visibleUntil) {
            if (hintInstantiated != null) {
                HideInteractionHint();
            }

            return;
        }

        if (hintInstantiated == null) {
            hintInstantiated = Instantiate(prefab);
            hintInstantiated.SetActive(false);
        }

        var hintInstantiatedTransform = hintInstantiated.transform;

        var dir = (originTransformPosition - interactablePositionPosition).normalized;
        var displayPosition = interactablePositionPosition + distanceFromInteractable * dir;
        hintInstantiatedTransform.SetPositionAndRotation(displayPosition, Quaternion.identity);
        hintInstantiatedTransform.LookAt(originTransform);

        hintInstantiated.SetActive(true);
    }

    public void HideInteractionHint() {
        Destroy(hintInstantiated);
        hintInstantiated = null;
    }

}