using System.Collections.Generic;
using UnityEngine;

public class CarriedBusTub : MonoBehaviour
{
    [SerializeField] GameObject bussedGlassesParent;

    private int totalGlassware;
    private int maxGlassware = 25;

    private List<GameObject> bussedGlassesObjects = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in bussedGlassesParent.transform)
        {
            bussedGlassesObjects.Add(child.gameObject);
        }

        ShowBussedGlasses(totalGlassware);
    }

    void OnEnable()
    {
        Actions.ResetLevel += ResetState;
    }

    void OnDisable()
    {
        Actions.ResetLevel -= ResetState;
    }

    public void ShowBussedGlasses(int glasses)
    {
        totalGlassware = glasses;
        if (glasses == 0)
        {
            HideAllBussedGlasses();
            return;
        }

        float glassesToShow = glasses / (float)maxGlassware * bussedGlassesObjects.Count;
        int maxCount = Mathf.Clamp((int) Mathf.Ceil(glassesToShow), 0, bussedGlassesObjects.Count);
        for (int i = 0; i < maxCount; i++)
        {
            bussedGlassesObjects[i].SetActive(true);
        }
    }

    public void HideAllBussedGlasses()
    {
        for (int i = 0; i < bussedGlassesObjects.Count; i++)
        {
            bussedGlassesObjects[i].SetActive(false);
        }
    }

    private void ResetState()
    {
        HideAllBussedGlasses();
    }
}
