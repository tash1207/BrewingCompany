using UnityEngine;

public class BeerGlass : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer beerFill;

    [Header("Settings")]
    [SerializeField] float beerDepletionRate = 10f;
    [SerializeField] float beerAmountDeemedEmpty = 0.35f;

    void Update()
    {
        if (PauseControl.Instance.GameIsPaused) { return; }
        
        if (beerFill != null && beerFill.size.y > 0)
        {
            beerFill.size = new Vector2(
                beerFill.size.x,
                Mathf.Clamp(beerFill.size.y - (beerDepletionRate / 100 * Time.deltaTime), 0, 1));
        }
    }

    public bool PickUp(PlayerInventory inventory)
    {
        if (inventory.IsCarryingPoop())
        {
            AlertControl.Instance.ShowAlert("Throw away poop before picking up glassware.", 2f);
            return false;
        }
        else if (inventory.IsCarryingMaxGlassware())
        {
            AlertControl.Instance.ShowAlert("Already carrying 5 glasses.", 2f);
            return false;
        }
        else if (beerFill.size.y > beerAmountDeemedEmpty)
        {
            AlertControl.Instance.ShowAlert("That beer isn't empty yet!", 2f);
            return false;
        }
        else
        {
            gameObject.SetActive(false);
            ResetBeerFill();
            Actions.OnBeerGrabbed(gameObject);
            return true;
        }
    }

    void ResetBeerFill()
    {
        beerFill.size = new Vector2(1, 1);
    }
}
