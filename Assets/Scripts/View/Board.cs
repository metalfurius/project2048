using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform board;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float totalAnimationDuration = 0.2f;

    private D2048 d2048;
    private Score score;
    private BoardRenderer boardRenderer;
    private InputHandler inputHandler;
    private TileCreator tileCreator;
    private BoardAnimator boardAnimator;

    public void Setup(D2048 d2048)
    {
        this.d2048 = d2048;
        this.inputHandler = GetComponent<InputHandler>();
        this.tileCreator = new TileCreator(board, tilePrefab, d2048.Board.GetLength(0), d2048.Board.GetLength(1));
        this.boardRenderer = new BoardRenderer(board, d2048);
        this.score = new Score(scoreText, d2048);
        this.boardAnimator = new BoardAnimator(this, inputHandler, tileCreator, boardRenderer, score, totalAnimationDuration);

        tileCreator.CreateBoard();
        boardRenderer.RenderBoard();
        board.GetComponent<GridLayoutGroup>().constraintCount = d2048.Board.GetLength(0);

        if (inputHandler != null)
        {
            inputHandler.OnMove += MoveBoard;
        }
    }

    private void MoveBoard(Vector2Int direction)
    {
        d2048.MoveTiles(direction);
        d2048.GenerateNewTile();
        StartCoroutine(boardAnimator.AnimateMovements(d2048.GetMovements()));
    }

    public int GetTileValue(Vector2Int position)
    {
        return d2048.Board[position.x, position.y];
    }
}
