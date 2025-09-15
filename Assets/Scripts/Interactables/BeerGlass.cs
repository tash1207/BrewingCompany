using UnityEngine;

public class BeerGlass : Interactable
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

    public override bool Interact(PlayerInventory inventory)
    {
        if (inventory.IsCarryingPoop())
        {
            AlertControl.Instance.ShowShortAlert("Throw away poop before picking up glassware.");
            return false;
        }
        else if (!IsEmpty())
        {
            AlertControl.Instance.ShowShortAlert("That beer isn't empty yet!");
            return false;
        }
        else
        {
            gameObject.SetActive(false);
            ResetBeerFill();
            Actions.OnItemPickedUp(gameObject);
            return true;
        }
    }

    public override int GetPriority()
    {
        return IsEmpty() ? Priorities.BeerGlassEmpty : Priorities.BeerGlassFull;
    }

    public bool IsEmpty()
    {
        return beerFill.size.y < beerAmountDeemedEmpty;
    }

    public void SetBeerFill(float fill)
    {
        beerFill.size = new Vector2(1, fill);
    }

    public void ResetBeerFill()
    {
        SetBeerFill(1f);
    }
}
