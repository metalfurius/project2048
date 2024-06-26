using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform board;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private TextMeshProUGUI scoreText;

    private D2048 d2048;
    private Score score;
    private BoardRenderer boardRenderer;
    private InputHandler inputHandler;
    private TileCreator tileCreator;

    public void Setup(D2048 d2048)
    {
        this.d2048 = d2048;
        this.inputHandler = GetComponent<InputHandler>();
        this.tileCreator = new TileCreator(board, tilePrefab, d2048.board.GetLength(0), d2048.board.GetLength(1));
        this.boardRenderer = new BoardRenderer(board,d2048);
        this.score = new Score(scoreText,d2048);

        tileCreator.CreateBoard();
        boardRenderer.RenderBoard();
        board.GetComponent<GridLayoutGroup>().constraintCount = d2048.board.GetLength(0);

        if (inputHandler != null)
        {
            inputHandler.OnMove += MoveBoard;
        }
    }
    private void MoveBoard(Vector2Int direction)
    {
        d2048.MoveTiles(direction);
        boardRenderer.RenderBoard();
        score.UpdateScore();
    }
}
