using UnityEngine;

public class Movement
{
    public Vector2Int Start { get; private set; }
    public Vector2Int End { get; private set; }

    public Movement(Vector2Int start, Vector2Int end)
    {
        Start = start;
        End = end;
    }
}
