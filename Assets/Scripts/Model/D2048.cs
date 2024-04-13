using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2048
{
    public int score;
    public int[,] board;
    public D2048(int score,Vector2Int board)
    {
        this.score = score;
        this.board = new int[board.x,board.y];
    }
    public void MergeTiles(int mergedValue)
    {
        score += mergedValue;
    }

    public void GenerateNewTile()
    {
        int x = Random.Range(0, board.GetLength(0));
        int y = Random.Range(0, board.GetLength(1));

        board[x, y] = 2;
    }
}
