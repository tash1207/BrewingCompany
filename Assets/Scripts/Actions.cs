using System;
using UnityEngine;

public static class Actions
{
    // Game
    public static Action OnLevelStarted;
    public static Action OnLevelEnded;

    // Inventory
    public static Action<GameObject> OnBeerGrabbed;
    public static Action<int> OnGlasswareChanged;

    // Player Display Options
    public static Action<PlayerDisplayOptions.Shirt> OnChooseShirt;

    // Score
    public static Action<int> OnGlasswareCleared;

    // UI
    public static Action OnButtonClicked;
    public static Action OnButtonToggled;
}
