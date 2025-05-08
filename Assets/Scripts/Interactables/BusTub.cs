using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BusTub : MonoBehaviour
{
    [SerializeField] GameObject bussedGlassesParent;
    [SerializeField] GameObject statusCanvas;
    [SerializeField] TMP_Text statusText;

    private int totalGlassware;
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

        int clearedGlasses = inventory.ClearGlassware();
        if (clearedGlasses > 0)
        {
            totalGlassware += clearedGlasses;
            Actions.OnGlasswareCleared(clearedGlasses);
            AlertControl.Instance.ShowAlert(
                "Cleared " + clearedGlasses +
                (clearedGlasses == 1 ? " glass." : " glasses."), 2f);
            UpdateBusTubDisplay();
        }
        else
        {
            AlertControl.Instance.ShowAlert("I can bring empty glasses here.");
        }
    }

    private void UpdateBusTubDisplay()
    {
        statusText.text = totalGlassware + " / " + maxGlassware;
        ShowBussedGlasses();
    }

    private void ShowBussedGlasses()
    {
        int maxCount = Mathf.Clamp(totalGlassware, 0, bussedGlassesObjects.Count);
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
        totalGlassware = 0;
        UpdateBusTubDisplay();
        HideAllBussedGlasses();
    }
}
