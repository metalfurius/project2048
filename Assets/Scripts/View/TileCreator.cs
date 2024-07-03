using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileCreator
{
    private Transform board;
    private GameObject tilePrefab;
    private int rows;
    private int columns;

    public TileCreator(Transform board, GameObject tilePrefab, int rows, int columns)
    {
        this.board = board;
        this.tilePrefab = tilePrefab;
        this.rows = rows;
        this.columns = columns;
    }

    public void CreateBoard()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                CreateTile(i, j);
            }
        }
    }

    private void CreateTile(int x, int y)
    {
        GameObject newTile = GameObject.Instantiate(tilePrefab, Vector2.zero, Quaternion.identity);
        newTile.transform.SetParent(board, false);
        newTile.name = $"Tile_{x}_{y}";
    }

    public GameObject CreateTileInstance(Vector3 position, int value, Transform parent, Vector2 size, Color color)
    {
        GameObject tileInstance = GameObject.Instantiate(tilePrefab, position, Quaternion.identity, parent);
        tileInstance.GetComponentInChildren<TextMeshProUGUI>().text = value > 0 ? value.ToString() : "";
        RectTransform rectTransform = tileInstance.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
        Image tileImage = tileInstance.GetComponentInChildren<Image>();
        if (tileImage != null)
        {
            tileImage.color = color;
        }
        return tileInstance;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        int index = gridPosition.x * columns + gridPosition.y;
        return board.GetChild(index).position;
    }

    public Vector2 GetTileSize()
    {
        if (board.childCount > 0)
        {
            RectTransform rectTransform = board.GetChild(0).GetComponent<RectTransform>();
            return rectTransform.sizeDelta;
        }
        return Vector2.zero;
    }

    public Color GetTileColor(Vector2Int gridPosition)
    {
        int index = gridPosition.x * columns + gridPosition.y;
        Image tileImage = board.GetChild(index).GetComponentInChildren<Image>();
        return tileImage != null ? tileImage.color : Color.white;
    }
}
