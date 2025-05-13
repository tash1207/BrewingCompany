using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private Vector3 zone1Position = new Vector3(-18, 0, -10);
    private Vector3 zone2Position = new Vector3(0, 0, -10);

    void Update()
    {
        if (playerTransform.position.x < -9)
        {
            transform.position = zone1Position;
        }
        else
        {
            transform.position = zone2Position;
        }
    }
}
