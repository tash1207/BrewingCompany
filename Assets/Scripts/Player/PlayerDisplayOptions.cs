using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerDisplayOptions : MonoBehaviour
{
    public enum Shirt
    {
        Green,
        Yellow
    }

    [SerializeField] SpriteLibrary spriteLibrary;
    [SerializeField] SpriteLibraryAsset greenShirt;
    [SerializeField] SpriteLibraryAsset yellowShirt;

    private const string shirtSettingName = "StoryShirt";

    void Start()
    {
        WearShirt(PlayerPrefs.GetInt(shirtSettingName, -1));
    }

    void OnEnable()
    {
        Actions.OnChooseShirt += ChooseShirt;
    }

    void OnDisable()
    {
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
        // Default to the green shirt.
        SpriteLibraryAsset newShirt = greenShirt;
        switch (shirtIndex)
        {
            case 0:
                newShirt = greenShirt;
                break;
            case 1:
                newShirt = yellowShirt;
                break;
        }
        spriteLibrary.spriteLibraryAsset = newShirt;
    }
}
