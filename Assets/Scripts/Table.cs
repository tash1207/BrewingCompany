using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] bool isHorizontalTable;

    public float getXRange()
    {
        return isHorizontalTable ? 1.14f : 0.7f;
    }

    public float getYRange()
    {
        return isHorizontalTable ? 0.42f : 0.8f;
    }
}
