using UnityEngine;

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
                CreateTile();
            }
        }
    }

    private void CreateTile()
    {
        GameObject newTile = GameObject.Instantiate(tilePrefab, Vector2.zero, Quaternion.identity);
        newTile.transform.SetParent(board, false);
    }
}
