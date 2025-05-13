using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadZone1()
    {
        GameManager.Instance.LoadZone1();
    }

    public void LoadZone2()
    {
        GameManager.Instance.LoadZone2();
    }

    public void LoadZones1and2()
    {
        GameManager.Instance.LoadZones1and2();
    }
}
