using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerInventory playerInventory;

    void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    /*
     * Determines which of the hit objects should be interacted based on
     * priority and proximity.
     */
    public void Interact(RaycastHit2D[] hits)
    {
        GameObject highestPriorityObject = null;
        int highestPriority = Priorities.NoPriority;
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.TryGetComponent(out Interactable interactable))
                {
                    if (interactable.GetPriority() > highestPriority)
                    {
                        highestPriorityObject = hitObject;
                        highestPriority = interactable.GetPriority();
                    }
                }
            }
        }
        if (highestPriorityObject != null)
        {
            Interact(highestPriorityObject);
        }
    }

    void Interact(GameObject item)
    {
        if (item.TryGetComponent(out BeerGlass beerGlass))
        {
            if (CanPickUpBeerGlass())
            {
                PickUp(beerGlass);
            }
        }
        else if (item.TryGetComponent(out Interactable interactable))
        {
            interactable.Interact(playerInventory);
        }
    }

    bool CanPickUpBeerGlass()
    {
        if (!playerInventory.IsCarryingBusTub() && !SkillsManager.Instance.AllowRiskyPickup
            && playerInventory.IsCarryingMaxGlassware())
        {
            AlertControl.Instance.ShowShortAlert(
                "Already carrying " + SkillsManager.Instance.MaxGlasses + " glasses.");
            return false;
        }
        if (playerInventory.IsCarryingBusTub() && playerInventory.NumGlasses >= BusTub.MaxGlassware)
        {
            AlertControl.Instance.ShowShortAlert("This bus tub can't hold any more glasses.");
            return false;
        }
        return true;
    }

    void PickUp(BeerGlass beerGlass)
    {
        if (beerGlass.Interact(playerInventory))
        {
            if (playerInventory.IsCarryingBusTub())
            {
                playerInventory.ChangeBusTubGlasswareCount(1);
                Actions.OnGlasswareCleared(1);
            }
            else
            {
                OnPickedUpGlass();
            }
        }
    }

    void OnPickedUpGlass()
    {
        // Check for random chance that player drops all glasses.
        if (SkillsManager.Instance.AllowRiskyPickup &&
            playerInventory.IsCarryingMaxGlassware() &&
            MaybeDropGlassware())
        {
            // TODO: Keep track of glasses broken.
            playerInventory.SetNumGlasses(0);
            SFXManager.Instance.PlayGlassBreaking();
            AlertControl.Instance.ShowShortAlert("Dropped all glasses!");
        }
        else
        {
            playerInventory.ChangeNumGlasses(1);
            if (SkillsManager.Instance.AllowRiskyPickup &&
                playerInventory.NumGlasses == SkillsManager.Instance.MaxGlasses)
            {
                AlertControl.Instance.ShowLongAlert(
                    "WARNING: Trying to carry more glasses may result in dropping them.");
            }
        }
    }
    
    bool MaybeDropGlassware()
    {
        int percentChanceOfDropping =
            5 + (8 * (playerInventory.NumGlasses - SkillsManager.Instance.MaxGlasses));
        return Random.Range(1, 101) <= percentChanceOfDropping;
    }
}
