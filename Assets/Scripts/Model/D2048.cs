using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2048
{
    public int score;
    public int[,] board;
    public int numberedTiles;

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
        numberedTiles++;
    }
    public void MoveTiles(Vector2Int direction)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            for (int i = 1; i < board.GetLength(0); i++) // Comenzamos desde la segunda fila
            {
                if (board[i, j] != 0) // Ignora las casillas vacías
                {
                    // Busca la casilla superior no vacía
                    int targetRow = i - 1;
                    while (targetRow >= 0 && board[targetRow, j] == 0)
                    {
                        targetRow--;
                    }

                    // Si encontramos una casilla con el mismo valor, combina las casillas
                    if (targetRow >= 0 && board[targetRow, j] == board[i, j])
                    {
                        board[targetRow, j] *= 2;
                        board[i, j] = 0;
                        MergeTiles(board[targetRow, j]); // Actualiza la puntuación
                    }
                    else
                    {
                        // Si no hay combinación, mueve la casilla actual a la posición superior
                        board[targetRow + 1, j] = board[i, j];
                        if (targetRow + 1 != i)
                        {
                            board[i, j] = 0;
                        }
                    }
                }
            }
        }
    }
}
