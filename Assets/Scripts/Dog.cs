using System.Collections;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] GameObject heartBubble;
    [SerializeField] GameObject dogPoopPrefab;

    private WaypointMover waypointMover;
    private bool hasTriedToPoop;
    private bool isShowingHeartBubble;

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

    public void Pet()
    {
        if (isShowingHeartBubble) { return; }
        StartCoroutine(ShowHeartBubbleForSeconds(1.5f));
    }

    IEnumerator ShowHeartBubbleForSeconds(float seconds)
    {
        isShowingHeartBubble = true;
        heartBubble.SetActive(true);
        yield return new WaitForSeconds(seconds);
        heartBubble.SetActive(false);
        isShowingHeartBubble = false;
    }
}
