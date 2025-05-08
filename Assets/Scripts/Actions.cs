using System;
using UnityEngine;

public static class Actions
{
    // Game
    public static Action OnLevelStarted;
    public static Action OnLevelEnded;
    public static Action ResetLevel;

    // Inventory
    public static Action<GameObject> OnItemPickedUp;
    public static Action<int> OnGlasswareChanged;
    public static Action<int> OnPoopCountChanged;

    // Player Display Options
    public static Action<PlayerDisplayOptions.Shirt> OnChooseShirt;

    // Score
    public static Action<int> OnGlasswareCleared;
    public static Action<int> OnPoopsThrownAway;

    // Settings
    public static Action<bool> OnBackgroundMusicToggled;

    // UI
    public static Action OnButtonClicked;
    public static Action OnButtonToggled;
}
