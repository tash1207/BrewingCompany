using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerDisplayOptions : MonoBehaviour
{
    [SerializeField] SpriteLibrary spriteLibrary;
    [SerializeField] SpriteLibraryAsset greenShirt;
    [SerializeField] SpriteLibraryAsset yellowShirt;

    void Awake()
    {
        LevelManager.Instance.OnChooseGreenShirt += WearGreenShirt;
        LevelManager.Instance.OnChooseYellowShirt += WearYellowShirt;
    }

    void OnDestroy()
    {
        LevelManager.Instance.OnChooseGreenShirt -= WearGreenShirt;
        LevelManager.Instance.OnChooseYellowShirt -= WearYellowShirt;
    }

    private void WearGreenShirt()
    {
        spriteLibrary.spriteLibraryAsset = greenShirt;
    }

    private void WearYellowShirt()
    {
        spriteLibrary.spriteLibraryAsset = yellowShirt;
    }
}
