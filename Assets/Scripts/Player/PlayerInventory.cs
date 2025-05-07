using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumGlasses { get; private set; }

    private int maxGlasses = 5;

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
            // TODO: Update HUD
        }
    }
    
    public void ChangeGlasses(int amount)
    {
        NumGlasses += amount;
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
        Actions.OnGlasswareChanged(NumGlasses);
    }

    public bool IsCarryingGlassware()
    {
        return NumGlasses > 0;
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
}
