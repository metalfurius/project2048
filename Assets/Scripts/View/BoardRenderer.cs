using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoardRenderer
{
    private Transform board;
    private D2048 d2048;
    private TileColors tileColors;

    public BoardRenderer(Transform board, D2048 d2048)
    {
        this.board = board;
        this.d2048 = d2048;
        this.tileColors = new TileColors();
    }

    public void RenderBoard()
    {
        for (int i = 0; i < d2048.board.GetLength(0); i++)
        {
            for (int j = 0; j < d2048.board.GetLength(1); j++)
            {
                Vector2Int gridPosition = new Vector2Int(i, j);
                int tileValue = d2048.board[i, j];
                SetTileValue(gridPosition, tileValue);
                SetTileColor(gridPosition, tileValue);
            }
        }
    }

    public void SetTileValue(Vector2Int gridPosition, int value)
    {
        int index = gridPosition.x * d2048.board.GetLength(1) + gridPosition.y;
        Transform tileTransform = board.GetChild(index);
        tileTransform.GetComponentInChildren<TextMeshProUGUI>().text = value > 0 ? value.ToString() : "";
    }

    public void SetTileColor(Vector2Int gridPosition, int value)
    {
        int index = gridPosition.x * d2048.board.GetLength(1) + gridPosition.y;
        Transform tileTransform = board.GetChild(index);
        Image tileImage = tileTransform.GetComponentInChildren<Image>();
        if (tileImage != null)
        {
            tileImage.color = tileColors.GetColor(value);
        }
    }
}
