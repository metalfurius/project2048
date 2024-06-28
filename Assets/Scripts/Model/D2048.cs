using System.Collections.Generic;
using UnityEngine;

public class D2048
{
    public int score;
    public int[,] board;
    public int numberedTiles;

    public static readonly Vector2Int Up = new(0, 1);
    public static readonly Vector2Int Down = new(0, -1);
    public static readonly Vector2Int Left = new(-1, 0);
    public static readonly Vector2Int Right = new(1, 0);

    private List<Movement> movements;

    public D2048(Vector2Int boardSize)
    {
        board = new int[boardSize.x, boardSize.y];
        score = 0;
        movements = new List<Movement>();
    }

    public D2048(Vector2Int boardSize, int startingCells) : this(boardSize)
    {
        for (int i = 0; i < startingCells; i++)
        {
            GenerateNewTile();
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void GenerateNewTile()
    {
        List<Vector2Int> emptyTiles = new();
        GetEmptyTiles(emptyTiles);

        if (emptyTiles.Count <= 0)
        {
            return;
        }

        GetNewRandomTileValue(emptyTiles, out Vector2Int randomTile, out int newTileValue);
        board[randomTile.x, randomTile.y] = newTileValue;
        numberedTiles++;
    }

    private void GetNewRandomTileValue(List<Vector2Int> emptyTiles, out Vector2Int randomTile, out int newTileValue)
    {
        randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
        int maxValueForNewTile = Mathf.Max(2, GetMaxTileValue() / 4);
        newTileValue = GetRandomTileValue(maxValueForNewTile);
    }

    private void GetEmptyTiles(List<Vector2Int> emptyTiles)
    {
        // Recopila todas las casillas vacías
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                if (board[x, y] == 0)
                {
                    emptyTiles.Add(new Vector2Int(x, y));
                }
            }
        }
    }

    private int GetMaxTileValue()
    {
        int maxTileValue = 0;
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                if (board[x, y] > maxTileValue)
                {
                    maxTileValue = board[x, y];
                }
            }
        }
        return maxTileValue;
    }

    private int GetRandomTileValue(int maxValueForNewTile)
    {
        List<int> possibleValues = new List<int>();
        int value = 2;
        while (value <= maxValueForNewTile)
        {
            possibleValues.Add(value);
            value *= 2;
        }
        return possibleValues[Random.Range(0, possibleValues.Count)];
    }

    public void MoveTiles(Vector2Int direction)
    {
        movements.Clear(); 

        if (direction == Up)
        {
            MoveUp();
        }
        else if (direction == Down)
        {
            MoveDown();
        }
        else if (direction == Right)
        {
            MoveRight();
        }
        else if (direction == Left)
        {
            MoveLeft();
        }
        GenerateNewTile();
    }
    public void MoveTilesTESTS(Vector2Int direction)
    {
        movements.Clear();

        if (direction == Up)
        {
            MoveUp();
        }
        else if (direction == Down)
        {
            MoveDown();
        }
        else if (direction == Right)
        {
            MoveRight();
        }
        else if (direction == Left)
        {
            MoveLeft();
        }
    }

    public List<Movement> GetMovements()
    {
        return new List<Movement>(movements);
    }

    private void MoveUp()
    {
        for (int column = 0; column < board.GetLength(1); column++)
        {
            for (int row = 1; row < board.GetLength(0); row++)
            {
                if (!IsTileEmpty(row, column))
                {
                    int targetRow = FindTargetRow(row, column, Up);

                    if (CanMerge(row, column, targetRow, column))
                    {
                        MergeTiles(row, column, targetRow, column);
                    }
                    else
                    {
                        MoveTile(row, column, targetRow + 1, column);
                    }
                }
            }
        }
    }

    private void MoveDown()
    {
        for (int column = 0; column < board.GetLength(1); column++)
        {
            for (int row = board.GetLength(0) - 2; row >= 0; row--)
            {
                if (!IsTileEmpty(row, column))
                {
                    int targetRow = FindTargetRow(row, column, Down);

                    if (CanMerge(row, column, targetRow, column))
                    {
                        MergeTiles(row, column, targetRow, column);
                    }
                    else
                    {
                        MoveTile(row, column, targetRow - 1, column);
                    }
                }
            }
        }
    }

    private void MoveRight()
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int column = board.GetLength(1) - 2; column >= 0; column--)
            {
                if (!IsTileEmpty(row, column))
                {
                    int targetColumn = FindTargetColumn(row, column, Right);

                    if (CanMerge(row, column, row, targetColumn))
                    {
                        MergeTiles(row, column, row, targetColumn);
                    }
                    else
                    {
                        MoveTile(row, column, row, targetColumn - 1);
                    }
                }
            }
        }
    }

    private void MoveLeft()
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int column = 1; column < board.GetLength(1); column++)
            {
                if (!IsTileEmpty(row, column))
                {
                    int targetColumn = FindTargetColumn(row, column, Left);

                    if (CanMerge(row, column, row, targetColumn))
                    {
                        MergeTiles(row, column, row, targetColumn);
                    }
                    else
                    {
                        MoveTile(row, column, row, targetColumn + 1);
                    }
                }
            }
        }
    }

    private bool IsTileEmpty(int row, int column)
    {
        return board[row, column] == 0;
    }

    private bool CanMerge(int sourceRow, int sourceColumn, int targetRow, int targetColumn)
    {
        return targetRow >= 0 && targetColumn >= 0 &&
               targetRow < board.GetLength(0) && targetColumn < board.GetLength(1) &&
               board[targetRow, targetColumn] == board[sourceRow, sourceColumn];
    }

    private int FindTargetRow(int currentRow, int column, Vector2Int direction)
    {
        int targetRow = currentRow - direction.y;
        while (targetRow >= 0 && targetRow < board.GetLength(0) && board[targetRow, column] == 0)
        {
            targetRow -= direction.y;
        }
        return targetRow;
    }

    private int FindTargetColumn(int row, int currentColumn, Vector2Int direction)
    {
        int targetColumn = currentColumn + direction.x;
        while (targetColumn >= 0 && targetColumn < board.GetLength(1) && board[row, targetColumn] == 0)
        {
            targetColumn += direction.x;
        }
        return targetColumn;
    }

    private void MergeTiles(int sourceRow, int sourceColumn, int targetRow, int targetColumn)
    {
        board[targetRow, targetColumn] *= 2;
        board[sourceRow, sourceColumn] = 0;
        AddScore(board[targetRow, targetColumn]);
    }

    private void MoveTile(int sourceRow, int sourceColumn, int targetRow, int targetColumn)
    {
        if (targetRow != sourceRow || targetColumn != sourceColumn)
        {
            board[targetRow, targetColumn] = board[sourceRow, sourceColumn];
            board[sourceRow, sourceColumn] = 0;

            movements.Add(new Movement(new Vector2Int(sourceRow, sourceColumn), new Vector2Int(targetRow, targetColumn)));
        }
    }
}

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
