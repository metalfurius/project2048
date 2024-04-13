using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2048
{
    public int score;
    public int[,] board;

    public static readonly Vector2Int Up = new Vector2Int(0, 1);
    public static readonly Vector2Int Down = new Vector2Int(0, -1);
    public static readonly Vector2Int Left = new Vector2Int(-1, 0);
    public static readonly Vector2Int Right = new Vector2Int(1, 0);
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
    public void MoveTiles(Vector2Int direction)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            MoveTilesInColumn(j);
        }
    }
    private void MoveTilesInColumn(int column)
    {
        for (int i = 1; i < board.GetLength(0); i++)
        {
            MoveTileUpwardsIfPossible(i, column);
        }
    }
    private void MoveTileUpwardsIfPossible(int row, int column)
    {
        if (board[row, column] != 0)
        {
            MoveTileUpwards(row, column);
            CombineTilesIfPossible(row - 1, column, row, column);
        }
    }
    private void MoveTileUpwards(int row, int column)
    {
        int currentRow = row;

        while (currentRow > 0 && board[currentRow - 1, column] == 0)
        {
            MoveTileOneStepUp(currentRow, column);
            currentRow--;
        }
    }
    private void MoveTileOneStepUp(int row, int column)
    {
        board[row - 1, column] = board[row, column];
        board[row, column] = 0;
    }
    private void CombineTilesIfPossible(int targetRow, int column, int movingRow, int movingColumn)
    {
        if (targetRow >= 0 && board[targetRow, column] == board[movingRow, movingColumn])
        {
            board[targetRow, column] *= 2;
            board[movingRow, movingColumn] = 0;
        }
    }
}
