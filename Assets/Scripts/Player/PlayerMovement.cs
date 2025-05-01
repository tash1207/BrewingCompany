using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] PlayerInventory playerInventory;

    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private float moveSpeed = 3.5f;

    private Vector2 lookDirection = new Vector2(0, -1);
    private float interactionDistance = 1.3f;

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

        if (!Mathf.Approximately(moveInput.x, 0f) || !Mathf.Approximately(moveInput.y, 0f))
        {
            lookDirection.Set(moveInput.x, moveInput.y);
            lookDirection.Normalize();

            animator.SetFloat("LookX", lookDirection.x);
            animator.SetFloat("LookY", lookDirection.y);
        }
    }

    void OnInteract(InputValue value)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            lookDirection,
            interactionDistance,
            LayerMask.GetMask("Interactable"));
        if (hit.collider != null)
        {
            playerInventory.Interact(hit.collider.gameObject);
        }
    }
}
