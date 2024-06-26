using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size = new(7, 7);

    [SerializeField]
    private int startingCells = 7;
    private void Start()
    {
        D2048 d2048 = new(new Vector2Int(size.x,size.y), startingCells);
        Board board = FindObjectOfType<Board>();
        board.Setup(d2048);
    }
}
