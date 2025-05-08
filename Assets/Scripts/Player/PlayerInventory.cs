using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumGlasses { get; private set; }
    public int NumPoops { get; private set; }

    private int maxGlasses = 5;

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
        NumGlasses += amount;
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
        Actions.OnGlasswareChanged(NumGlasses);
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

    public int ClearGlassware()
    {
        int glassesCleared = NumGlasses;
        NumGlasses = 0;
        Actions.OnGlasswareChanged(NumGlasses);
        return glassesCleared;
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
