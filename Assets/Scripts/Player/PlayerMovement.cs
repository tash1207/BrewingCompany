using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animatorDown;
    [SerializeField] Animator animatorSide;
    [SerializeField] Animator animatorUp;
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
        SetActiveAnimator(lookDirection);

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

    void Update() {
        SetCarryingAnimation(playerInventory.IsCarryingBusTub());
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
            // lookDirection.Normalize();
            SetActiveAnimator(lookDirection);

            SetWalkingAnimation(true);
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
            new Vector2(transform.position.x, transform.position.y + 1.1f),
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
        SetActiveAnimator(lookDirection);
    }

    private void SetWalkingAnimation(bool value)
    {
        animatorDown.SetBool("Body_Walk", value);
        animatorDown.SetBool("Legs_Walk", value);

        animatorDown.SetBool("Body_Idle", !value);
        animatorDown.SetBool("Legs_Idle", !value);

        animatorSide.SetBool("Body_Walk", value);
        animatorSide.SetBool("Legs_Walk", value);

        animatorSide.SetBool("Body_Idle", !value);
        animatorSide.SetBool("Legs_Idle", !value);

        animatorUp.SetBool("Body_Walk", value);
        animatorUp.SetBool("Legs_Walk", value);

        animatorUp.SetBool("Body_Idle", !value);
        animatorUp.SetBool("Legs_Idle", !value);
    }

    private void SetCarryingAnimation(bool value)
    {
        animatorDown.SetBool("Body_Carry", value);
        animatorSide.SetBool("Body_Carry", value);
        animatorUp.SetBool("Body_Carry", value);
    }

    private void SetActiveAnimator(Vector2 direction)
    {
        if (direction.x == 0 && direction.y == -1)
        {
            animatorSide.gameObject.SetActive(false);
            animatorUp.gameObject.SetActive(false);
            animatorDown.gameObject.SetActive(true);
        }
        else if (direction.x != 0)
        {
            animatorSide.gameObject.transform.localScale =
                direction.x < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

            animatorDown.gameObject.SetActive(false);
            animatorUp.gameObject.SetActive(false);
            animatorSide.gameObject.SetActive(true);
        }
        else if (direction.x == 0 && direction.y == 1)
        {
            animatorDown.gameObject.SetActive(false);
            animatorSide.gameObject.SetActive(false);
            animatorUp.gameObject.SetActive(true);
        }
        else
        {
            // Default position.
            animatorSide.gameObject.SetActive(false);
            animatorUp.gameObject.SetActive(false);
            animatorDown.gameObject.SetActive(true);
        }
    }
}
