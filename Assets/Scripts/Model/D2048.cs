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
                    int targetRow = FindTargetRow(i, j, direction); // Busca la casilla objetivo

                    if (targetRow >= 0 && board[targetRow, j] == board[i, j]) // Comprueba si hay combinación
                    {
                        MergeTilesAndUpdateScore(i, j, targetRow); 
                    }
                    else
                    {
                        MoveTile(i, j, targetRow + 1); // Mueve la casilla a la posición objetivo
                    }
                }
            }
        }
    }

    private int FindTargetRow(int currentRow, int column, Vector2Int direction)
    {
        int targetRow = currentRow - 1;
        while (targetRow >= 0 && board[targetRow, column] == 0)
        {
            targetRow--;
        }
        return targetRow;
    }

    private void MergeTilesAndUpdateScore(int currentRow, int column, int targetRow)
    {
        board[targetRow, column] *= 2;
        board[currentRow, column] = 0;
        MergeTiles(board[targetRow, column]); // Actualiza la puntuación
    }

    private void MoveTile(int currentRow, int column, int targetRow)
    {
        board[targetRow, column] = board[currentRow, column];
        if (targetRow != currentRow)
        {
            board[currentRow, column] = 0;
        }
    }
}
