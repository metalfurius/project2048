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
        if (direction == Up)
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
                            MoveTile(i, j, targetRow + 1, j); // Mueve la casilla a la posición objetivo
                        }
                    }
                }
            }
        }
        else if (direction == Down)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                for (int i = board.GetLength(0) - 2; i >= 0; i--) // Comenzamos desde la penúltima fila
                {
                    if (board[i, j] != 0) // Ignora las casillas vacías
                    {
                        int targetRow = FindTargetRow(i, j, direction); // Busca la casilla objetivo

                        if (targetRow < board.GetLength(0) && board[targetRow, j] == board[i, j]) // Comprueba si hay combinación
                        {
                            MergeTilesAndUpdateScore(i, j, targetRow);
                        }
                        else
                        {
                            MoveTile(i, j, targetRow - 1, j); // Mueve la casilla a la posición objetivo
                        }
                    }
                }
            }
        }
        else if (direction == Right)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = board.GetLength(1) - 2; j >= 0; j--) // Comenzamos desde la penúltima columna
                {
                    if (board[i, j] != 0) // Ignora las casillas vacías
                    {
                        int targetColumn = FindTargetColumn(i, j, direction); // Busca la columna objetivo

                        if (targetColumn < board.GetLength(1) && board[i, targetColumn] == board[i, j]) // Comprueba si hay combinación
                        {
                            MergeTilesAndUpdateScore(i, j, i, targetColumn);
                        }
                        else
                        {
                            MoveTile(i, j, i, targetColumn - 1); // Mueve la casilla a la posición objetivo
                        }
                    }
                }
            }
        }
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

    private void MergeTilesAndUpdateScore(int currentRow, int currentColumn, int targetRow)
    {
        board[targetRow, currentColumn] *= 2;
        board[currentRow, currentColumn] = 0;
        MergeTiles(board[targetRow, currentColumn]); // Actualiza la puntuación
    }

    private void MergeTilesAndUpdateScore(int currentRow, int currentColumn, int targetRow, int targetColumn)
    {
        board[targetRow, targetColumn] *= 2;
        board[currentRow, currentColumn] = 0;
        MergeTiles(board[targetRow, targetColumn]); // Actualiza la puntuación
    }

    private void MoveTile(int currentRow, int currentColumn, int targetRow, int targetColumn)
    {
        board[targetRow, targetColumn] = board[currentRow, currentColumn];
        if (targetRow != currentRow || targetColumn != currentColumn)
        {
            board[currentRow, currentColumn] = 0;
        }
    }
}
