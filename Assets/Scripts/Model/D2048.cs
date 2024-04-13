using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2048
{
    public int score;
    public D2048(int score)
    {
        this.score = score;
    }
    public void MergeTiles(int mergedValue)
    {
        score += mergedValue;
    }
}
