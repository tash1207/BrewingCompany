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

    private Vector2 initialPosition;
    private Vector2 lookDirection = new Vector2(0, -1);
    private float interactionDistance = 1.3f;

    private bool isPaused;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PausePlayerMovement();
    }

    void Start()
    {
        initialPosition = transform.position;
    }

    void OnEnable()
    {
        Actions.OnLevelStarted += ResumePlayerMovement;
        Actions.OnLevelEnded += PausePlayerMovement;
        Actions.ResetLevel += ResetPlayerPosition;
    }

    void OnDisable()
    {
        Actions.OnLevelStarted -= ResumePlayerMovement;
        Actions.OnLevelEnded -= PausePlayerMovement;
        Actions.ResetLevel -= ResetPlayerPosition;
    }

    void FixedUpdate()
    {
        if (isPaused) { return; }
        
        rb2d.velocity = moveInput * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        if (isPaused) { return; }

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
        if (isPaused) { return; }

        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y + 0.75f),
            lookDirection,
            interactionDistance,
            LayerMask.GetMask("Interactable"));
        if (hit.collider != null)
        {
            playerInventory.Interact(hit.collider.gameObject);
        }
    }

    public void PausePlayerMovement()
    {
        rb2d.velocity = Vector2.zero;
        moveInput = Vector2.zero;
        isPaused = true;
    }

    public void ResumePlayerMovement()
    {
        isPaused = false;
    }

    private void ResetPlayerPosition()
    {
        transform.position = initialPosition;
        lookDirection.Set(0, -1);
        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
    }
}
