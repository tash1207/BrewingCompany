using UnityEngine;

public class BusTubTable : MonoBehaviour
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
}
