using System;
using UnityEngine;

public static class Actions
{
    // Game
    public static Action OnLevelStarted;
    public static Action OnLevelEnded;
    public static Action OnLevelPaused;
    public static Action OnLevelResumed;
    public static Action TogglePauseMenu;
    public static Action ResetLevel;

    // Inventory
    public static Action<GameObject> OnItemPickedUp;
    public static Action<int> OnGlasswareChanged;
    public static Action<int> OnBusTubGlasswareCountChanged;
    public static Action<int> OnPoopCountChanged;

    // Player Display Options
    public static Action<PlayerDisplayOptions.Shirt> OnChooseShirt;

    // Player Stats
    public static Action<ExpManager> OnExpChanged;
    public static Action<int> OnLevelChanged;

    // Score
    public static Action<int> OnGlasswareCleared;
    public static Action<int> OnPoopsThrownAway;

    // Settings
    public static Action<bool> OnBackgroundMusicToggled;

    // Story
    public static Action OnStartNewStory;

    // UI
    public static Action OnButtonClicked;
    public static Action OnButtonToggled;
}
