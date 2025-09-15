using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumGlasses { get; private set; }
    public int NumPoops { get; private set; }
    public int NumBusTubs { get; private set; }

    [SerializeField] GameObject[] carriedBusTubs;

    void OnEnable()
    {
        Actions.ResetLevel += ResetState;
    }

    void OnDisable()
    {
        Actions.ResetLevel -= ResetState;
    }

    public void SetNumGlasses(int amount)
    {
        NumGlasses = amount;
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
        Actions.OnGlasswareChanged(NumGlasses);
    }

    public void ChangeNumGlasses(int amount)
    {
        SetNumGlasses(NumGlasses + amount);
    }

    public void ChangeBusTubGlasswareCount(int amount)
    {
        NumBusTubs = 1;
        NumGlasses += amount;
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
        Actions.OnBusTubGlasswareCountChanged(NumGlasses);

        UpdateBusTubGlasswareUI(false);
    }

    public void ChangePoopCount(int amount)
    {
        NumPoops += amount;
        NumPoops = Mathf.Clamp(NumPoops, 0, int.MaxValue);
        Actions.OnPoopCountChanged(NumPoops);
    }

    // Dropping off carried glassware into a bus tub.
    public int ClearGlassware(int amount)
    {
        int glassesCleared = amount;
        NumGlasses -= amount;
        Actions.OnGlasswareChanged(NumGlasses);
        return glassesCleared;
    }

    public int ClearPoops()
    {
        int poopsDiscarded = NumPoops;
        NumPoops = 0;
        Actions.OnPoopCountChanged(NumPoops);
        return poopsDiscarded;
    }

    // Dropping off glassware from one bus tub to another.
    public void DropOffGlassware(int amount)
    {
        NumGlasses -= amount;
        NumGlasses = Mathf.Clamp(NumGlasses, 0, int.MaxValue);
        Actions.OnBusTubGlasswareCountChanged(NumGlasses);

        UpdateBusTubGlasswareUI(true);
    }

    public void CarryBusTub()
    {
        foreach (GameObject busTub in carriedBusTubs)
        {
            busTub.SetActive(true);
        }
    }

    void UpdateBusTubGlasswareUI(bool firstHide)
    {
        foreach (GameObject busTub in carriedBusTubs)
        {
            if (busTub.TryGetComponent(out CarriedBusTub carriedBusTub))
            {
                if (firstHide && NumGlasses != 0)
                {
                    carriedBusTub.HideAllBussedGlasses();
                }
                carriedBusTub.ShowBussedGlasses(NumGlasses);
            }
        }
    }

    public void DropOffBusTub()
    {
        foreach (GameObject busTub in carriedBusTubs)
        {
            busTub.SetActive(false);
        }

        NumBusTubs = 0;
        SetNumGlasses(0);
    }

    public bool IsCarryingBusTub()
    {
        return NumBusTubs > 0;
    }

    public bool IsCarryingGlassware()
    {
        return NumGlasses > 0;
    }

    public bool IsCarryingPoop()
    {
        return NumPoops > 0;
    }

    public bool IsCarryingMaxGlassware()
    {
        return NumGlasses >= SkillsManager.Instance.MaxGlasses;
    }

    void ResetState()
    {
        NumGlasses = 0;
        NumPoops = 0;
        NumBusTubs = 0;
    }
}
