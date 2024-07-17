using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileCreator
{
    private readonly Transform board;
    private readonly GameObject tilePrefab;
    private readonly int rows;
    private readonly int columns;
    private readonly TileColors tileColors;

    public TileCreator(Transform board, GameObject tilePrefab, int rows, int columns)
    {
        this.board = board;
        this.tilePrefab = tilePrefab;
        this.rows = rows;
        this.columns = columns;
        this.tileColors = new TileColors();
    }

    public void CreateBoard()
    {
        for (var _i = 0; _i < rows; _i++)
        {
            for (var _j = 0; _j < columns; _j++)
            {
                CreateTile(_i, _j);
            }
        }
    }

    private void CreateTile(int x, int y)
    {
        var _newTile = Object.Instantiate(tilePrefab, Vector2.zero, Quaternion.identity);
        _newTile.transform.SetParent(board, false);
        _newTile.name = $"Tile_{x}_{y}";
    }

    public GameObject CreateTileInstance(Vector3 position, int value, Transform parent, Vector2 size, Color color)
    {
        var _tileInstance = Object.Instantiate(tilePrefab, position, Quaternion.identity, parent);
        _tileInstance.GetComponentInChildren<TextMeshProUGUI>().text = value > 0 ? value.ToString() : "";
        var _rectTransform = _tileInstance.GetComponent<RectTransform>();
        _rectTransform.sizeDelta = size;
        var _tileImage = _tileInstance.GetComponentInChildren<Image>();
        if (_tileImage)
        {
            _tileImage.color = color;
        }
        return _tileInstance;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        var _index = gridPosition.x * columns + gridPosition.y;
        return board.GetChild(_index).position;
    }

    public Vector2 GetTileSize()
    {
        if (board.childCount <= 0) return Vector2.zero;
        var _rectTransform = board.GetChild(0).GetComponent<RectTransform>();
        return _rectTransform.sizeDelta;
    }

    public Color GetTileColor(Vector2Int gridPosition)
    {
        var _index = gridPosition.x * columns + gridPosition.y;
        var _tileImage = board.GetChild(_index).GetComponentInChildren<Image>();
        return _tileImage ? _tileImage.color : Color.white;
    }

    public Color GetColorForValue(int value)
    {
        return tileColors.GetColor(value);
    }
}
