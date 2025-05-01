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

    void OnEnable()
    {
        Actions.OnChooseShirt += WearShirt;
    }

    void OnDisable()
    {
        Actions.OnChooseShirt -= WearShirt;
    }

    private void WearShirt(Shirt shirt)
    {
        // Default to the green shirt.
        SpriteLibraryAsset newShirt = greenShirt;
        switch (shirt)
        {
            case Shirt.Green:
                newShirt = greenShirt;
                break;
            case Shirt.Yellow:
                newShirt = yellowShirt;
                break;
        }
        spriteLibrary.spriteLibraryAsset = newShirt;
    }
}
