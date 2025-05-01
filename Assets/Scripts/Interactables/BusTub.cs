using UnityEngine;

public class BusTub : MonoBehaviour
{
    public void Interact(PlayerInventory inventory)
    {
        int clearedGlasses = inventory.ClearGlassware();
        if (clearedGlasses > 0)
        {
            Actions.OnGlasswareCleared(clearedGlasses);
            AlertControl.Instance.ShowAlert("Cleared " + clearedGlasses + " glasses.");
        }
        else
        {
            AlertControl.Instance.ShowAlert("I can bring empty glasses here.");
        }
    }
}
