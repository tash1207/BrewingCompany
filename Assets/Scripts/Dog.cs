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
            float randomX = Random.Range(-0.15f, 0.15f);
            float randomY = Random.Range(0f, 0.25f);
            Vector2 poopPosition = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
            Instantiate(dogPoopPrefab, poopPosition, Quaternion.identity);
        }
        hasTriedToPoop = true;
    }
}
