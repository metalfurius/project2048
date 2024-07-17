using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoardRenderer
{
    private readonly Transform board;
    private readonly D2048 d2048;
    private readonly TileColors tileColors;

    public BoardRenderer(Transform board, D2048 d2048)
    {
        this.board = board;
        this.d2048 = d2048;
        this.tileColors = new TileColors();
    }

    public void RenderBoard()
    {
        for (var _i = 0; _i < d2048.Board.GetLength(0); _i++)
        {
            for (var _j = 0; _j < d2048.Board.GetLength(1); _j++)
            {
                var _gridPosition = new Vector2Int(_i, _j);
                var _tileValue = d2048.Board[_i, _j];
                SetTileValue(_gridPosition, _tileValue);
                SetTileColor(_gridPosition, _tileValue);
            }
        }
    }

    public void SetTileValue(Vector2Int gridPosition, int value)
    {
        var _index = gridPosition.x * d2048.Board.GetLength(1) + gridPosition.y;
        var _tileTransform = board.GetChild(_index);
        _tileTransform.GetComponentInChildren<TextMeshProUGUI>().text = value > 0 ? value.ToString() : "";
    }

    public void SetTileColor(Vector2Int gridPosition, int value)
    {
        var _index = gridPosition.x * d2048.Board.GetLength(1) + gridPosition.y;
        var _tileTransform = board.GetChild(_index);
        var _tileImage = _tileTransform.GetComponentInChildren<Image>();
        if (_tileImage != null)
        {
            _tileImage.color = tileColors.GetColor(value);
        }
    }
}
