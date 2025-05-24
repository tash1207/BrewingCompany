using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BusTub : MonoBehaviour
{
    [SerializeField] GameObject bussedGlassesParent;
    [SerializeField] GameObject statusCanvas;
    [SerializeField] TMP_Text statusText;

    public int TotalGlassware;
    private int maxGlassware = 25;

    private List<GameObject> bussedGlassesObjects = new List<GameObject>();
    
    void Start()
    {
        foreach(Transform child in bussedGlassesParent.transform)
        {
            bussedGlassesObjects.Add(child.gameObject);
        }

        UpdateBusTubDisplay();
    }

    void OnEnable()
    {
        Actions.ResetLevel += ResetState;
    }

    void OnDisable()
    {
        Actions.ResetLevel -= ResetState;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            statusCanvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            statusCanvas.SetActive(false);
        }
    }

    public void Interact(PlayerInventory inventory)
    {
        if (inventory.IsCarryingPoop())
        {
            AlertControl.Instance.ShowAlert("Poop goes in the trash can, not the bus tub.");
            return;
        }
        if (IsFull())
        {
            AlertControl.Instance.ShowAlert("This bus tub is full.");
            return;
        }
        if (inventory.NumGlasses == 0)
        {
            AlertControl.Instance.ShowAlert("I can bring empty glasses here.");
            return;
        }

        ClearGlassware(inventory);
    }

    public bool PickUp(PlayerInventory inventory)
    {
        if (inventory.IsCarryingPoop())
        {
            AlertControl.Instance.ShowAlert("Poop goes in the trash can, not the bus tub.");
            return false;
        }
        if (inventory.IsCarryingGlassware() && !inventory.IsCarryingBusTub())
        {
            Interact(inventory);
            return false;
        }
        if (inventory.IsCarryingGlassware() && inventory.IsCarryingBusTub())
        {
            if (IsFull())
            {
                AlertControl.Instance.ShowAlert("This bus tub is full.");
                return false;
            }
            else
            {
                CombineBusTubGlassware(inventory);
                return false;
            }
        }
        if (!inventory.IsCarryingGlassware() && !inventory.IsCarryingBusTub())
        {
            gameObject.SetActive(false);
            Actions.OnItemPickedUp(gameObject);
            inventory.CarryBusTub();
            return true;
        }
        if (!inventory.IsCarryingGlassware() && inventory.IsCarryingBusTub())
        {
            AlertControl.Instance.ShowAlert("I already have an empty bus tub.");
            return false;
        }
        return false;
    }

    private void ClearGlassware(PlayerInventory inventory)
    {
        int clearedGlasses;
        if (TotalGlassware + inventory.NumGlasses > maxGlassware)
        {
            clearedGlasses = inventory.ClearGlassware(maxGlassware - TotalGlassware);
        }
        else
        {
            clearedGlasses = inventory.ClearAllGlassware();
        }
        TotalGlassware += clearedGlasses;
        Actions.OnGlasswareCleared(clearedGlasses);
        SFXManager.Instance.PlayDropOffClip();
        AlertControl.Instance.ShowAlert(
            "Cleared " + clearedGlasses +
            (clearedGlasses == 1 ? " glass." : " glasses."), 2f);
        UpdateBusTubDisplay();
    }

    private void CombineBusTubGlassware(PlayerInventory inventory)
    {
        int droppedOffGlasses;
        if (TotalGlassware + inventory.NumGlasses > maxGlassware)
        {
            droppedOffGlasses = maxGlassware - TotalGlassware;
            inventory.DropOffGlassware(droppedOffGlasses);
        }
        else
        {
            droppedOffGlasses = inventory.NumGlasses;
            inventory.DropOffGlassware(droppedOffGlasses);
        }
        TotalGlassware += droppedOffGlasses;
        SFXManager.Instance.PlayDropOffClip();
        AlertControl.Instance.ShowAlert(
            "Dropped off " + droppedOffGlasses +
            (droppedOffGlasses == 1 ? " glass." : " glasses."), 2f);
        UpdateBusTubDisplay();
    }

    private void UpdateBusTubDisplay()
    {
        statusText.text = TotalGlassware + " / " + maxGlassware;
        ShowBussedGlasses();
    }

    public void ClearAndUpdateBusTubDisplay()
    {
        HideAllBussedGlasses();
        UpdateBusTubDisplay();
    }

    private bool IsFull()
    {
        return TotalGlassware == maxGlassware;
    }

    private void ShowBussedGlasses()
    {
        int maxCount = Mathf.Clamp(TotalGlassware, 0, bussedGlassesObjects.Count);
        for (int i = 0; i < maxCount; i++)
        {
            bussedGlassesObjects[i].SetActive(true);
        }
    }

    private void HideAllBussedGlasses()
    {
        for (int i = 0; i < bussedGlassesObjects.Count; i++)
        {
            bussedGlassesObjects[i].SetActive(false);
        }
    }

    private void ResetState()
    {
        TotalGlassware = 0;
        UpdateBusTubDisplay();
        HideAllBussedGlasses();
    }
}
