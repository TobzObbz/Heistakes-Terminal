using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameHandler : MonoBehaviour
{
    public static MinigameHandler Instance;

    [SerializeField] private InputActionReference joystickMove;
    [SerializeField] private GameObject player;
        public GameObject GetPlayer() => player;

    private Vector2 moveInput;
    private Vector2 adjustedMove;
    private Rigidbody2D rb;

    private bool startGame = false;

    private bool canMove = true;
        public bool SetCanMove(bool _canMove) => canMove = _canMove;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        canMove = true;
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
        if (!canMove) return;

        if (!startGame)
        {
            startGame = true;

            MinigameCanvas.Instance.StartTimer();
        }

        //moveInput = Vector2.zero;

        //if (Gamepad.current != null)
        //{
        //    moveInput = Gamepad.current.leftStick.ReadValue();
        //}
        //else if (Joystick.current != null)
        //{
        //    float x = Joystick.current.stick.x.ReadValue();
        //    float y = Joystick.current.stick.y.ReadValue();
        //    moveInput = new Vector2(x, y);
        //}

        moveInput = context.ReadValue<Vector2>();   

        adjustedMove = new Vector2(-moveInput.y, moveInput.x).normalized;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        rb.MovePosition(rb.position + adjustedMove * 5f * Time.fixedDeltaTime);
    }
}