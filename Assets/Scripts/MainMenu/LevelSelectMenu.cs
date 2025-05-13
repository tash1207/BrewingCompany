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

        // TODO: Use a loop and better logic for displaying level items.
        if (levelsParentObject.transform.childCount > 1 &&
            StoryModeManager.Instance.CurrentDay >= 2)
        {
            levelsParentObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (levelsParentObject.transform.childCount > 2 &&
            StoryModeManager.Instance.CurrentDay >= 3)
        {
            levelsParentObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
