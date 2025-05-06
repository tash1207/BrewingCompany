using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetElementToSelectOnInteraction : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable elementToSelect;

    [SerializeField] bool showVisualization;

    private Color navigationColor = Color.cyan;

    void OnDrawGizmos()
    {
        if (!showVisualization) { return; }
        if (elementToSelect == null) { return; }

        Gizmos.color = navigationColor;
        Gizmos.DrawLine(gameObject.transform.position, elementToSelect.gameObject.transform.position);
    }

    void Reset()
    {
        eventSystem = FindObjectOfType<EventSystem>();

        if (eventSystem == null)
        {
            Debug.Log("No EventSystem found in scene.", this);
        }
    }

    public void JumpToElement()
    {
        eventSystem.SetSelectedGameObject(elementToSelect.gameObject);
    }
}
