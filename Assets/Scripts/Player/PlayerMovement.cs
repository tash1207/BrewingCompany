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
        Actions.OnLevelResumed += ResumePlayerMovement;
        Actions.OnLevelPaused += PausePlayerMovement;
        Actions.ResetLevel += ResetPlayerPosition;
    }

    void OnDisable()
    {
        Actions.OnLevelResumed -= ResumePlayerMovement;
        Actions.OnLevelPaused -= PausePlayerMovement;
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

            SetWalkingAnimation(true);
            animator.SetFloat("LookX", lookDirection.x);
            animator.SetFloat("LookY", lookDirection.y);
        }
        else
        {
            SetWalkingAnimation(false);
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

    void OnPause(InputValue value)
    {
        Actions.TogglePauseMenu();
    }

    public void PausePlayerMovement()
    {
        rb2d.velocity = Vector2.zero;
        moveInput = Vector2.zero;
        SetWalkingAnimation(false);

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

    private void SetWalkingAnimation(bool value)
    {
        animator.SetBool("Body_Walk", value);
        animator.SetBool("Legs_Walk", value);

        animator.SetBool("Body_Idle", !value);
        animator.SetBool("Legs_Idle", !value);
    }
}
