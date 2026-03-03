using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference joystickMove;
    [SerializeField] private GameObject player;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        joystickMove.action.performed += OnJoystickMove;

        joystickMove.action.Enable();

        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnJoystickMove(InputAction.CallbackContext context)
    {
        //StartCoroutine(MovePlayer(context));
    }

    private IEnumerator MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        while (move != Vector2.zero)
        {
            move = context.ReadValue<Vector2>();
            Debug.Log("Joystick Value: " + move);
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + move * 200f * Time.fixedDeltaTime);
            //player.transform.Translate(new Vector2(move.x * 100, move.y * 100) * Time.deltaTime * 5f);
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        Vector2 move = Joystick.current.stick.ReadValue();
        Debug.Log("Joystick Value: " + move);
        rb.MovePosition(rb.position + move * 200f * Time.fixedDeltaTime);
        //player.transform.Translate(new Vector2(move.x * 100, move.y * 100) * Time.deltaTime * 5f);
    }
}   