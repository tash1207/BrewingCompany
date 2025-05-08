using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject levelsParentObject;

    void Start()
    {
        if (levelsParentObject.transform.childCount < 1) { return; }
        
        // Always show the zone 1 level.
        levelsParentObject.transform.GetChild(0).gameObject.SetActive(true);

        if (levelsParentObject.transform.childCount > 1 &&
            PlayerPrefs.GetInt("StoryCurrentDay", -1) > 0)
        {
            levelsParentObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
