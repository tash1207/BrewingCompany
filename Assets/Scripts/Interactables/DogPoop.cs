using UnityEngine;

public class DogPoop : MonoBehaviour
{
    public bool PickUp(PlayerInventory inventory)
    {
        if (inventory.IsCarryingBusTub())
        {
            AlertControl.Instance.ShowAlert("Put the bus tub away before picking up dog poop.", 2.5f);
            return false;
        }
        else if (inventory.IsCarryingGlassware())
        {
            AlertControl.Instance.ShowAlert("Drop off glasses before picking up dog poop.", 2.5f);
            return false;
        }
        else
        {
            Actions.OnItemPickedUp(gameObject);
            return true;
        }
    }
}
