using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private float moveSpeed = 3.5f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveInput * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
