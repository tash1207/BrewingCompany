using UnityEngine;

public class BusTubTable : Interactable
{
    [SerializeField] BusTub busTub;

    public void Interact(PlayerInventory inventory)
    {
        if (inventory.IsCarryingBusTub())
        {
            busTub.TotalGlassware = inventory.NumGlasses;
            busTub.ClearAndUpdateBusTubDisplay();
            busTub.gameObject.SetActive(true);
            inventory.DropOffBusTub();
            return;
        }
    }

    public override int GetPriority()
    {
        return Priorities.BusTubTable;
    }
}
