using System.Collections;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform waypointParent;

    [Header("Settings")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float waitTime;
    [SerializeField] bool loopWaypoints = true;

    public bool IsWaiting;
    
    private Transform[] waypoints;
    private int currentWaypointIndex;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        waypoints = new Transform[waypointParent.childCount];

        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

    void Update()
    {
        if (PauseControl.Instance.GameIsPaused || IsWaiting) {
            animator.SetBool("isWalking", false);
            return;
        }

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        Vector2 direction = (transform.position - target.position).normalized;

        spriteRenderer.flipX = direction.x < 0;
        animator.SetFloat("LookX", direction.x);
        animator.SetFloat("LookY", direction.y);
        animator.SetBool("isWalking", direction.magnitude > 0f);

        transform.position =
            Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < 0.05f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        IsWaiting = true;
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(waitTime);

        currentWaypointIndex = loopWaypoints ?
            (currentWaypointIndex + 1) % waypoints.Length :
            Mathf.Min(currentWaypointIndex + 1, waypoints.Length - 1);

        IsWaiting = false;
    }
}
