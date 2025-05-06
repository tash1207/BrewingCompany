using UnityEngine;

public class UIButton : MonoBehaviour
{
    public void OnClick()
    {
        Actions.OnButtonClicked();
    }

    public void OnToggle()
    {
        Actions.OnButtonToggled();
    }
}
