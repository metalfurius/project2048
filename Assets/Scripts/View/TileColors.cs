using System.Collections.Generic;
using UnityEngine;

public class TileColors
{
    private Dictionary<int, Color> tileColors;

    public TileColors()
    {
        InitializeTileColors();
    }

    private void InitializeTileColors()
    {
        tileColors = new Dictionary<int, Color>
        {
            { 2, new Color(0.93f, 0.89f, 0.85f) }, // Light beige
            { 4, new Color(0.93f, 0.88f, 0.78f) }, // Light brown
            { 8, new Color(0.95f, 0.69f, 0.47f) }, // Light orange
            { 16, new Color(0.96f, 0.58f, 0.39f) }, // Darker orange
            { 32, new Color(0.96f, 0.49f, 0.37f) }, // Dark orange
            { 64, new Color(0.96f, 0.37f, 0.23f) }, // Red orange
            { 128, new Color(0.93f, 0.81f, 0.44f) }, // Gold
            { 256, new Color(0.78f, 0.63f, 0.20f) }, // Dark gold
            { 512, new Color(0.69f, 0.55f, 0.15f) }, // Darker gold
            { 1024, new Color(0.60f, 0.49f, 0.13f) }, // Even darker gold
            { 2048, new Color(0.49f, 0.38f, 0.10f) }  // Dark brown
        };
    }

    public Color GetColor(int value)
    {
        if (tileColors.TryGetValue(value, out Color color))
        {
            return color;
        }
        return Color.white;
    }
}
