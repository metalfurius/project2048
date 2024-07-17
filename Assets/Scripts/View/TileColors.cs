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
            { 0, Color.white }, // White
            { 2, new Color(0.93f, 0.89f, 0.85f) }, // Very light brown
            { 4, new Color(0.92f, 0.75f, 0.6f) }, // Light brown
            { 8, new Color(0.93f, 0.62f, 0.48f) }, // Light orange
            { 16, new Color(0.94f, 0.5f, 0.37f) }, // Orange
            { 32, new Color(0.95f, 0.38f, 0.27f) }, // Dark orange
            { 64, new Color(0.96f, 0.27f, 0.18f) }, // Dark red
            { 128, new Color(0.85f, 0.73f, 0.43f) }, // Gold
            { 256, new Color(0.92f, 0.6f, 0.29f) }, // Orange gold
            { 512, new Color(0.93f, 0.47f, 0.15f) }, // Dark orange gold
            { 1024, new Color(0.95f, 0.34f, 0.02f) }, // Dark red gold
            { 2048, new Color(1f, 0.84f, 0) }  // Bright gold
        };
    }

    public Color GetColor(int value)
    {
        return tileColors.TryGetValue(value, out var _color) ? _color : Color.white;
    }
}