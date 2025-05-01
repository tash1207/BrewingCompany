using System;

public static class Actions
{
    // Game
    public static Action OnLevelStarted;
    
    // Inventory
    public static Action<int> OnGlasswareChanged;

    // Player Display Options
    public static Action<PlayerDisplayOptions.Shirt> OnChooseShirt;

    // Score
    public static Action<int> OnGlasswareCleared;
}
