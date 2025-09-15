using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool Interact(PlayerInventory inv);

    public abstract int GetPriority();
}
