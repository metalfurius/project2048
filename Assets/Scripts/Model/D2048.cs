using System.Collections.Generic;
using UnityEngine;

public class D2048
{
    public int Score;
    public readonly int[,] Board;
    public int NumberedTiles;

    public static readonly Vector2Int Up = new(0, 1);
    public static readonly Vector2Int Down = new(0, -1);
    public static readonly Vector2Int Left = new(-1, 0);
    public static readonly Vector2Int Right = new(1, 0);

    private readonly List<Movement> movements;

    public D2048(Vector2Int boardSize)
    {
        Board = new int[boardSize.x, boardSize.y];
        Score = 0;
        movements = new List<Movement>();
    }

    public D2048(Vector2Int boardSize, int startingCells) : this(boardSize)
    {
        for (var _i = 0; _i < startingCells; _i++)
        {
            GenerateNewTile();
        }
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public Vector2Int GenerateNewTile()
    {
        List<Vector2Int> _emptyTiles = new();
        GetEmptyTiles(_emptyTiles);

        if (_emptyTiles.Count <= 0)
            return new Vector2Int(-1, -1);

        GetNewRandomTileValue(_emptyTiles, out var _randomTile, out var _newTileValue);
        Board[_randomTile.x, _randomTile.y] = _newTileValue;
        NumberedTiles++;
        return _randomTile;
    }

    private void GetNewRandomTileValue(List<Vector2Int> emptyTiles, out Vector2Int randomTile, out int newTileValue)
    {
        randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
        var _maxValueForNewTile = Mathf.Max(2, GetMaxTileValue() / 4);
        newTileValue = GetRandomTileValue(_maxValueForNewTile);
    }

    private void GetEmptyTiles(List<Vector2Int> emptyTiles)
    {
        for (var _x = 0; _x < Board.GetLength(0); _x++)
        {
            for (var _y = 0; _y < Board.GetLength(1); _y++)
            {
                if (Board[_x, _y] == 0)
                {
                    emptyTiles.Add(new Vector2Int(_x, _y));
                }
            }
        }
    }

    private int GetMaxTileValue()
    {
        var _maxTileValue = 0;
        for (var _x = 0; _x < Board.GetLength(0); _x++)
        {
            for (var _y = 0; _y < Board.GetLength(1); _y++)
            {
                if (Board[_x, _y] > _maxTileValue)
                {
                    _maxTileValue = Board[_x, _y];
                }
            }
        }
        return _maxTileValue;
    }

    private int GetRandomTileValue(int maxValueForNewTile)
    {
        var _possibleValues = new List<int>();
        var _value = 2;
        while (_value <= maxValueForNewTile)
        {
            _possibleValues.Add(_value);
            _value *= 2;
        }
        return _possibleValues[Random.Range(0, _possibleValues.Count)];
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
    }

    public List<Movement> GetMovements()
    {
        return new List<Movement>(movements);
    }

    private void MoveUp()
    {
        for (var _column = 0; _column < Board.GetLength(1); _column++)
        {
            for (var _row = 1; _row < Board.GetLength(0); _row++)
            {
                if (IsTileEmpty(_row, _column)) continue;
                var _targetRow = FindTargetRow(_row, _column, Up);

                if (CanMerge(_row, _column, _targetRow, _column))
                {
                    MergeTiles(_row, _column, _targetRow, _column);
                }
                else
                {
                    MoveTile(_row, _column, _targetRow + 1, _column);
                }
            }
        }
    }

    private void MoveDown()
    {
        for (var _column = 0; _column < Board.GetLength(1); _column++)
        {
            for (var _row = Board.GetLength(0) - 2; _row >= 0; _row--)
            {
                if (IsTileEmpty(_row, _column)) continue;
                var _targetRow = FindTargetRow(_row, _column, Down);

                if (CanMerge(_row, _column, _targetRow, _column))
                {
                    MergeTiles(_row, _column, _targetRow, _column);
                }
                else
                {
                    MoveTile(_row, _column, _targetRow - 1, _column);
                }
            }
        }
    }

    private void MoveRight()
    {
        for (var _row = 0; _row < Board.GetLength(0); _row++)
        {
            for (var _column = Board.GetLength(1) - 2; _column >= 0; _column--)
            {
                if (IsTileEmpty(_row, _column)) continue;
                var _targetColumn = FindTargetColumn(_row, _column, Right);

                if (CanMerge(_row, _column, _row, _targetColumn))
                {
                    MergeTiles(_row, _column, _row, _targetColumn);
                }
                else
                {
                    MoveTile(_row, _column, _row, _targetColumn - 1);
                }
            }
        }
    }

    private void MoveLeft()
    {
        for (var _row = 0; _row < Board.GetLength(0); _row++)
        {
            for (var _column = 1; _column < Board.GetLength(1); _column++)
            {
                if (IsTileEmpty(_row, _column)) continue;
                var _targetColumn = FindTargetColumn(_row, _column, Left);

                if (CanMerge(_row, _column, _row, _targetColumn))
                {
                    MergeTiles(_row, _column, _row, _targetColumn);
                }
                else
                {
                    MoveTile(_row, _column, _row, _targetColumn + 1);
                }
            }
        }
    }

    private bool IsTileEmpty(int row, int column)
    {
        return Board[row, column] == 0;
    }

    private bool CanMerge(int sourceRow, int sourceColumn, int targetRow, int targetColumn)
    {
        return targetRow >= 0 && targetColumn >= 0 &&
               targetRow < Board.GetLength(0) && targetColumn < Board.GetLength(1) &&
               Board[targetRow, targetColumn] == Board[sourceRow, sourceColumn];
    }

    private int FindTargetRow(int currentRow, int column, Vector2Int direction)
    {
        var _targetRow = currentRow - direction.y;
        while (_targetRow >= 0 && _targetRow < Board.GetLength(0) && Board[_targetRow, column] == 0)
        {
            _targetRow -= direction.y;
        }
        return _targetRow;
    }

    private int FindTargetColumn(int row, int currentColumn, Vector2Int direction)
    {
        var _targetColumn = currentColumn + direction.x;
        while (_targetColumn >= 0 && _targetColumn < Board.GetLength(1) && Board[row, _targetColumn] == 0)
        {
            _targetColumn += direction.x;
        }
        return _targetColumn;
    }

    private void MergeTiles(int sourceRow, int sourceColumn, int targetRow, int targetColumn)
    {
        Board[targetRow, targetColumn] *= 2;
        Board[sourceRow, sourceColumn] = 0;
        AddScore(Board[targetRow, targetColumn]);
        movements.Add(new Movement(new Vector2Int(sourceRow, sourceColumn), new Vector2Int(targetRow, targetColumn), true));
    }

    private void MoveTile(int sourceRow, int sourceColumn, int targetRow, int targetColumn)
    {
        if (targetRow == sourceRow && targetColumn == sourceColumn) return;
        Board[targetRow, targetColumn] = Board[sourceRow, sourceColumn];
        Board[sourceRow, sourceColumn] = 0;

        movements.Add(new Movement(new Vector2Int(sourceRow, sourceColumn), new Vector2Int(targetRow, targetColumn)));
    }
}
