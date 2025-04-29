using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumGlasses { get; private set; }

    public void TryPickUp(GameObject item)
    {
        if (item.TryGetComponent(out BeerGlass beerGlass))
        {
            if (beerGlass.PickUp())
            {
                ChangeGlasses(1);
            }
        }
    }
    
    public void ChangeGlasses(int amount)
    {
        NumGlasses += amount;
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
    }

    public bool IsCarryingGlassware()
    {
        return NumGlasses > 0;
    }
}
