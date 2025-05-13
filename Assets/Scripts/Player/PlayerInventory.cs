using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumGlasses { get; private set; }
    public int NumPoops { get; private set; }

    private int maxGlasses = 5;
    private bool allowRiskyPickup = false;

    void OnEnable()
    {
        Actions.ResetLevel += ResetState;
    }

    void OnDisable()
    {
        Actions.ResetLevel -= ResetState;
    }
    
    public void Interact(GameObject item)
    {
        if (item.TryGetComponent(out BeerGlass beerGlass))
        {
            TryPickUp(beerGlass);
        }
        else if (item.TryGetComponent(out BusTub busTub))
        {
            busTub.Interact(this);
        }
        else if (item.TryGetComponent(out DogPoop dogPoop))
        {
            TryPickUp(dogPoop);
        }
        else if (item.TryGetComponent(out Dog dog))
        {
            dog.Pet();
        }
        else if (item.TryGetComponent(out TrashCan trashCan))
        {
            trashCan.ThrowAwayTrashItems(this);
        }
    }
    
    void TryPickUp(BeerGlass beerGlass)
    {
        if (!allowRiskyPickup && IsCarryingMaxGlassware())
        {
            AlertControl.Instance.ShowAlert("Already carrying 5 glasses.", 2f);
            return;
        }
        if (beerGlass.PickUp(this))
        {
            ChangeGlasses(1);
        }
    }

    void TryPickUp(DogPoop dogPoop)
    {
        if (dogPoop.PickUp(this))
        {
            ChangePoopCount(1);
        }
    }
    
    public void ChangeGlasses(int amount)
    {
        if (allowRiskyPickup && IsCarryingMaxGlassware() && MaybeDropGlassware())
        {
            // TODO: Keep track of glasses broken.
            NumGlasses = 0;
            SFXManager.Instance.PlayGlassBreaking();
            AlertControl.Instance.ShowAlert("Dropped all glasses!", 2f);
        }
        else
        {
            NumGlasses += amount;
            if (allowRiskyPickup && NumGlasses == maxGlasses)
            {
                AlertControl.Instance.ShowAlert(
                    "WARNING: Trying to carry more glasses may result in dropping them.", 3.5f);
            }
        }
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
        Actions.OnGlasswareChanged(NumGlasses);
    }

    private bool MaybeDropGlassware()
    {
        int percentChanceOfDropping = 5 + (8 * (NumGlasses - maxGlasses));
        return Random.Range(0, 100) < percentChanceOfDropping;
    }

    public void ChangePoopCount(int amount)
    {
        NumPoops += amount;
        NumPoops = Mathf.Clamp(NumPoops, 0, int.MaxValue);
        Actions.OnPoopCountChanged(NumPoops);
    }

    public bool IsCarryingGlassware()
    {
        return NumGlasses > 0;
    }

    public bool IsCarryingPoop()
    {
        return NumPoops > 0;
    }

    public bool IsCarryingMaxGlassware()
    {
        return NumGlasses >= maxGlasses;
    }

    public int ClearGlassware(int amount)
    {
        int glassesCleared = amount;
        NumGlasses -= amount;
        Actions.OnGlasswareChanged(NumGlasses);
        return glassesCleared;
    }

    public int ClearAllGlassware()
    {
        return ClearGlassware(NumGlasses);
    }

    public int ClearPoops()
    {
        int poopsDiscarded = NumPoops;
        NumPoops = 0;
        Actions.OnPoopCountChanged(NumPoops);
        return poopsDiscarded;
    }

    private void ResetState()
    {
        NumGlasses = 0;
        NumPoops = 0;
    }
}
