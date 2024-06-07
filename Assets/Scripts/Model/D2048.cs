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

    public D2048(Vector2Int boardSize)
    {
        board = new int[boardSize.x, boardSize.y];
        score = 0;
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

        if (emptyTiles.Count > 0)
        {
            Vector2Int randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
            board[randomTile.x, randomTile.y] = 2;
            numberedTiles++;
        }
    }

    public void MoveTiles(Vector2Int direction)
    {
        if (direction == Up)
        {
            for (int column = 0; column < board.GetLength(1); column++)
            {
                for (int row = 1; row < board.GetLength(0); row++) // Comenzamos desde la segunda fila
                {
                    if (board[row, column] != 0) // Ignora las casillas vacías
                    {
                        int targetRow = FindTargetRow(row, column, direction); // Busca la casilla objetivo

                        if (targetRow >= 0 && board[targetRow, column] == board[row, column]) // Comprueba si hay combinación
                        {
                            MergeTiles(row, column, targetRow, column);
                        }
                        else
                        {
                            MoveTile(row, column, targetRow + 1, column); // Mueve la casilla a la posición objetivo
                        }
                    }
                }
            }
        }
        else if (direction == Down)
        {
            for (int column = 0; column < board.GetLength(1); column++)
            {
                for (int row = board.GetLength(0) - 2; row >= 0; row--) // Comenzamos desde la penúltima fila
                {
                    if (board[row, column] != 0) // Ignora las casillas vacías
                    {
                        int targetRow = FindTargetRow(row, column, direction); // Busca la casilla objetivo

                        if (targetRow < board.GetLength(0) && board[targetRow, column] == board[row, column]) // Comprueba si hay combinación
                        {
                            MergeTiles(row, column, targetRow, column);
                        }
                        else
                        {
                            MoveTile(row, column, targetRow - 1, column); // Mueve la casilla a la posición objetivo
                        }
                    }
                }
            }
        }
        else if (direction == Right)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = board.GetLength(1) - 2; column >= 0; column--) // Comenzamos desde la penúltima columna
                {
                    if (board[row, column] != 0) // Ignora las casillas vacías
                    {
                        int targetColumn = FindTargetColumn(row, column, direction); // Busca la columna objetivo

                        if (targetColumn < board.GetLength(1) && board[row, targetColumn] == board[row, column]) // Comprueba si hay combinación
                        {
                            MergeTiles(row, column, row, targetColumn);
                        }
                        else
                        {
                            MoveTile(row, column, row, targetColumn - 1); // Mueve la casilla a la posición objetivo
                        }
                    }
                }
            }
        }
        else if (direction == Left)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 1; column < board.GetLength(1); column++){
                    if (board[row, column] != 0)
                    {
                        int targetColumn = FindTargetColumn(row, column, direction);
                        if (targetColumn >= 0 && board[row, targetColumn] == board[row, column]) // Comprueba si hay combinación
                        {
                            MergeTiles(row, column, row, targetColumn);
                        }
                        else
                        {
                            MoveTile(row, column, row, targetColumn + 1); // Mueve la casilla a la posición objetivo
                        }
                    }
                }
            }
        }
        GenerateNewTile();
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
        AddScore(board[targetRow, targetColumn]); // Actualiza la puntuación
    }

    private void MoveTile(int sourceRow, int sourceColumn, int targetRow, int targetColumn)
    {
        board[targetRow, targetColumn] = board[sourceRow, sourceColumn];
        if (targetRow != sourceRow || targetColumn != sourceColumn)
        {
            board[sourceRow, sourceColumn] = 0;
        }
    }
}
