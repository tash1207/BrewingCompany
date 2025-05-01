using System;

public static class Actions
{
    // Inventory
    public static Action<int> OnGlasswareChanged;

    // Player Display Options
    public static Action<PlayerDisplayOptions.Shirt> OnChooseShirt;
}
