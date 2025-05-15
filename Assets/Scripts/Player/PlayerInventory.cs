using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumGlasses { get; private set; }
    public int NumPoops { get; private set; }
    public int NumBusTubs { get; private set; }

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
            if (SkillsManager.Instance.AllowBusTubPickup)
            {
                TryPickUp(busTub);
            }
            else
            {
                busTub.Interact(this);
            }
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
        else if (item.TryGetComponent(out BusTubTable busTubTable))
        {
            busTubTable.Interact(this);
        }
    }
    
    void TryPickUp(BeerGlass beerGlass)
    {
        if (!SkillsManager.Instance.AllowRiskyPickup && IsCarryingMaxGlassware())
        {
            AlertControl.Instance.ShowAlert(
                "Already carrying " + SkillsManager.Instance.MaxGlasses + " glasses.", 2f);
            return;
        }
        if (IsCarryingBusTub() && NumGlasses >= 25)
        {
            AlertControl.Instance.ShowAlert("This bus tub can't hold any more glasses.", 2f);
            return;
        }
        if (beerGlass.PickUp(this))
        {
            if (IsCarryingBusTub())
            {
                ChangeBusTubGlasswareCount(1);
                Actions.OnGlasswareCleared(1);
            }
            else
            {
                ChangeGlasses(1);
            }
        }
    }

    void TryPickUp(BusTub busTub)
    {
        if (busTub.PickUp(this))
        {
            ChangeBusTubGlasswareCount(busTub.TotalGlassware);
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
        if (SkillsManager.Instance.AllowRiskyPickup &&
            IsCarryingMaxGlassware() &&
            MaybeDropGlassware())
        {
            // TODO: Keep track of glasses broken.
            NumGlasses = 0;
            SFXManager.Instance.PlayGlassBreaking();
            AlertControl.Instance.ShowAlert("Dropped all glasses!", 2f);
        }
        else
        {
            NumGlasses += amount;
            if (SkillsManager.Instance.AllowRiskyPickup &&
                NumGlasses == SkillsManager.Instance.MaxGlasses)
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
        int percentChanceOfDropping = 5 + (8 * (NumGlasses - SkillsManager.Instance.MaxGlasses));
        return Random.Range(0, 100) < percentChanceOfDropping;
    }

    public void ChangeBusTubGlasswareCount(int amount)
    {
        NumBusTubs = 1;
        NumGlasses += amount;
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
        Actions.OnBusTubGlasswareCountChanged(NumGlasses);
    }

    public void ChangePoopCount(int amount)
    {
        NumPoops += amount;
        NumPoops = Mathf.Clamp(NumPoops, 0, int.MaxValue);
        Actions.OnPoopCountChanged(NumPoops);
    }

    public bool IsCarryingBusTub()
    {
        return NumBusTubs > 0;
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
        return NumGlasses >= SkillsManager.Instance.MaxGlasses;
    }

    // Dropping off carried glassware into a bus tub.
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

    // Dropping off glassware from one bus tub to another.
    public void DropOffGlassware(int amount)
    {
        NumGlasses -= amount;
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
        Actions.OnBusTubGlasswareCountChanged(NumGlasses);
    }

    public void DropOffBusTub()
    {
        NumBusTubs = 0;
        NumGlasses = 0;
        Actions.OnGlasswareChanged(0);
    }

    private void ResetState()
    {
        NumGlasses = 0;
        NumPoops = 0;
    }
}
