using System.Collections.Generic;
using UnityEngine;

public class BusTub : MonoBehaviour
{
    [SerializeField] GameObject bussedGlassesParent;

    private int totalGlassware;
    private int maxGlassware = 25;

    private List<GameObject> bussedGlassesObjects = new List<GameObject>();
    
    void Start()
    {
        foreach(Transform child in bussedGlassesParent.transform)
        {
            bussedGlassesObjects.Add(child.gameObject);
        }
    }

    public void Interact(PlayerInventory inventory)
    {
        int clearedGlasses = inventory.ClearGlassware();
        if (clearedGlasses > 0)
        {
            totalGlassware += clearedGlasses;
            Actions.OnGlasswareCleared(clearedGlasses);
            AlertControl.Instance.ShowAlert("Cleared " + clearedGlasses + " glasses.");
            ShowBussedGlasses();
        }
        else
        {
            AlertControl.Instance.ShowAlert("I can bring empty glasses here.");
        }
    }

    void ShowBussedGlasses()
    {
        int maxCount = Mathf.Clamp(totalGlassware, 0, bussedGlassesObjects.Count);
        for (int i = 0; i < maxCount; i++)
        {
            bussedGlassesObjects[i].SetActive(true);
        }
    }
}
