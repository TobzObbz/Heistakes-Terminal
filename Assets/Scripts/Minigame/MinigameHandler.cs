using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameHandler : MonoBehaviour
{
    public static MinigameHandler Instance;

    [SerializeField] private InputActionReference joystickMove;
    [SerializeField] private GameObject player;
        public GameObject GetPlayer() => player;

    private Vector2 moveInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        joystickMove.action.performed += OnJoystickMove;
        joystickMove.action.canceled += OnJoystickMove;

        joystickMove.action.Enable();

        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        joystickMove.action.performed -= OnJoystickMove;
        joystickMove.action.canceled -= OnJoystickMove;

        joystickMove.action.Disable();
    }

    private void OnJoystickMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * 5f * Time.fixedDeltaTime);
    }
}