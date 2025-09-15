using UnityEngine;

public class DogPoop : Interactable
{
    public override bool Interact(PlayerInventory inventory)
    {
        if (inventory.IsCarryingBusTub())
        {
            AlertControl.Instance.ShowAlert("Put the bus tub away before picking up dog poop.");
            return false;
        }
        else if (inventory.IsCarryingGlassware())
        {
            AlertControl.Instance.ShowAlert("Drop off glasses before picking up dog poop.");
            return false;
        }
        else
        {
            Actions.OnItemPickedUp(gameObject);
            inventory.ChangePoopCount(1);
            return true;
        }
    }

    public override int GetPriority()
    {
        return Priorities.DogPoop;
    }
}
