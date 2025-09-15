using UnityEngine;

public class BusTubTable : Interactable
{
    [SerializeField] BusTub busTub;

    public override bool Interact(PlayerInventory inventory)
    {
        if (inventory.IsCarryingBusTub())
        {
            busTub.TotalGlassware = inventory.NumGlasses;
            busTub.ClearAndUpdateBusTubDisplay();
            busTub.gameObject.SetActive(true);
            inventory.DropOffBusTub();
            return true;
        }
        return false;
    }

    public override int GetPriority()
    {
        return Priorities.BusTubTable;
    }
}
