using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class VRMovementController : MonoBehaviour
{
    //[SerializeField] private PlayerControllerInput playerController;
    public CharacterController characterController;
    public XRController leftXRController;
    public float speed = 1.0f;

    private Vector2 inputAxis;
    private InputAction moveAction;

    private void Awake()
    {
        // Assuming you have an action asset called "XRI Default Input Actions"
        //var actionAsset = leftXRController.GetComponent<ActionBasedController>().actionAsset;
       // moveAction = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        // Read the value from our action every frame
        inputAxis = moveAction.ReadValue<Vector2>();
        
        // Apply the input to the CharacterController
        Vector3 move = new Vector3(inputAxis.x, 0, inputAxis.y);
        move = transform.TransformDirection(move);
        
        // Move the CharacterController
        characterController.Move(move * Time.deltaTime * speed);
    }
}