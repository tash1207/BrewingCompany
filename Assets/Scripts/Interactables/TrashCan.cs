using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public void ThrowAwayTrashItems(PlayerInventory inventory)
    {
        if (inventory.IsCarryingGlassware())
        {
            AlertControl.Instance.ShowAlert(
                "Glassware goes in the bus tub, not the trash can.");
            return;
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
        }
        else
        {
            AlertControl.Instance.ShowAlert("It's a trash can.", 1.5f);
        }
    }
}
