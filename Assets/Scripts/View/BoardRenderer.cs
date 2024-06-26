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
                int _index = i * d2048.board.GetLength(1) + j;
                Transform _child = board.GetChild(_index);
                int tileValue = d2048.board[i, j];
                _child.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = tileValue > 0 ? tileValue.ToString() : "";
                SetTileColor(_child, tileValue);
            }
        }
    }

    private void SetTileColor(Transform tile, int value)
    {
        Image tileImage = tile.GetComponentInChildren<Image>();
        if (tileImage == null)
            return;

        tileImage.color = tileColors.GetColor(value);
    }
}
