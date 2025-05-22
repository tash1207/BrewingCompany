using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerDisplayOptions : MonoBehaviour
{
    public enum Shirt
    {
        Green,
        Yellow
    }

    [SerializeField] SpriteResolver spriteResolver;

    private const string shirtSettingName = "StoryShirt";

    void Start()
    {
        WearShirt(PlayerPrefs.GetInt(shirtSettingName, -1));
    }

    void OnEnable()
    {
        Actions.OnStartNewStory += ResetValues;
        Actions.OnChooseShirt += ChooseShirt;
    }

    void OnDisable()
    {
        Actions.OnStartNewStory -= ResetValues;
        Actions.OnChooseShirt -= ChooseShirt;
    }

    private void ChooseShirt(Shirt shirt)
    {
        switch (shirt)
        {
            case Shirt.Green:
                PlayerPrefs.SetInt(shirtSettingName, 0);
                WearShirt(0);
                break;
            case Shirt.Yellow:
                PlayerPrefs.SetInt(shirtSettingName, 1);
                WearShirt(1);
                break;
        }
    }

    private void WearShirt(int shirtIndex)
    {
        if (spriteResolver == null) { return; }
        switch (shirtIndex)
        {
            case 0:
                spriteResolver.SetCategoryAndLabel("Color", "Green");
                break;
            case 1:
                spriteResolver.SetCategoryAndLabel("Color", "Yellow");
                break;
            default:
                spriteResolver.SetCategoryAndLabel("Color", "White");
                break;
        }
    }

    private void ResetValues()
    {
        PlayerPrefs.SetInt(shirtSettingName, -1);
    }
}
