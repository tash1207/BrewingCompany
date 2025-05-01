using System.Globalization;
using UnityEngine;

public class BeerGlass : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer beerFill;

    [Header("Settings")]
    [SerializeField] float beerDepletionRate = 10f;
    [SerializeField] float beerAmountDeemedEmpty = 0.35f;

    void Update()
    {
        if (beerFill != null && beerFill.size.y > 0)
        {
            beerFill.size = new Vector2(
                beerFill.size.x,
                Mathf.Clamp(beerFill.size.y - (beerDepletionRate / 100 * Time.deltaTime), 0, 1));
        }
    }

    public bool PickUp()
    {
        if (beerFill.size.y > beerAmountDeemedEmpty)
        {
            AlertControl.Instance.ShowAlert("That beer isn't empty yet!", 2f);
            return false;
        }
        else
        {
            // TODO: Look into object pooling.
            Destroy(gameObject);
            return true;
        }
    }
}
