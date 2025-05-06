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

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private bool isWaiting;

    void Start()
    {
        waypoints = new Transform[waypointParent.childCount];

        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

    void Update()
    {
        if (PauseControl.Instance.GameIsPaused || isWaiting) { return; }

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];

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
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        currentWaypointIndex = loopWaypoints ?
            (currentWaypointIndex + 1) % waypoints.Length :
            Mathf.Min(currentWaypointIndex + 1, waypoints.Length - 1);

        isWaiting = false;
    }
}
