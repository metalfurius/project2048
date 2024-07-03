using UnityEngine;

public class Movement
{
    public Vector2Int Start { get; }
    public Vector2Int End { get; }
    public bool IsMerge { get; }

    public Movement(Vector2Int start, Vector2Int end, bool isMerge = false)
    {
        Start = start;
        End = end;
        IsMerge = isMerge;
    }
}
