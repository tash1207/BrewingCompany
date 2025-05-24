using System.Collections.Generic;
using UnityEngine;

public class CarriedBusTub : MonoBehaviour
{
    [SerializeField] GameObject bussedGlassesParent;

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

    private void ClearGlassware(PlayerInventory inventory)
    {
        UpdateBusTubDisplay();
    }

    private void UpdateBusTubDisplay()
    {
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
