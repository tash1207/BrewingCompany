using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadStoryMode()
    {
        GameManager.Instance.LoadStoryMode();
    }

    public void LoadZone1()
    {
        GameManager.Instance.LoadZone1();
    }
}
