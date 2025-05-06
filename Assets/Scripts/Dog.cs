using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] GameObject dogPoopPrefab;

    private WaypointMover waypointMover;
    private bool hasTriedToPoop;

    void Start()
    {
        waypointMover = GetComponent<WaypointMover>();
    }

    void Update()
    {
        if (waypointMover.IsWaiting && !hasTriedToPoop)
        {
            MaybePoop();
        }
        else if (!waypointMover.IsWaiting)
        {
            hasTriedToPoop = false;
        }
    }

    void MaybePoop()
    {
        if (Random.Range(0, 10) < 1)
        {
            Instantiate(dogPoopPrefab, transform.position, Quaternion.identity);
        }
        hasTriedToPoop = true;
    }
}
