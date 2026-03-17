using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField] private InputActionReference joystickAction;

    [SerializeField] private GameObject gate;

    private bool inCollision = false;

    private void OnEnable()
    {
        joystickAction.action.performed += JoystickActionPressed;
        joystickAction.action.canceled += JoystickActionPressed;

        joystickAction.action.Enable();
    }

    private void OnDisable()
    {
        joystickAction.action.performed -= JoystickActionPressed;
        joystickAction.action.canceled -= JoystickActionPressed;

        joystickAction.action.Disable();
    }

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>())
        {
            inCollision = true;
        }
    }

    private void JoystickActionPressed(InputAction.CallbackContext context)
    {
        if (inCollision)
        {
            gate.SetActive(false);
        }
    }
}