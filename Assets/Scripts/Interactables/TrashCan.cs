using UnityEngine;

public class TrashCan : Interactable
{
    public override bool Interact(PlayerInventory inventory)
    {
        if (inventory.IsCarryingGlassware())
        {
            AlertControl.Instance.ShowAlert(
                "Glassware goes in the bus tub, not the trash can.");
            return false;
        }

        if (inventory.IsCarryingPoop())
        {
            int clearedPoops = inventory.ClearPoops();
            if (clearedPoops > 0)
            {
                Actions.OnPoopsThrownAway(clearedPoops);
                AlertControl.Instance.ShowAlert(
                    "Threw away " + clearedPoops +
                    (clearedPoops == 1 ? " dog poop." : " dog poops."));
            }
            return true;
        }
        else
        {
            AlertControl.Instance.ShowShortAlert("It's a trash can.");
            return false;
        }
    }

    public override int GetPriority()
    {
        return Priorities.TrashCan;
    }
}
