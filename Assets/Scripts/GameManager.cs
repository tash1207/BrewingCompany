using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadZone1()
    {
        SceneManager.LoadScene("Zone 1");
    }
}
