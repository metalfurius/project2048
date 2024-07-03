using System.Collections;
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
        this.boardRenderer = new BoardRenderer(board, d2048);
        this.score = new Score(scoreText, d2048);

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
        StartCoroutine(AnimateMovements());
    }

    private IEnumerator AnimateMovements()
    {
        // Desactivar el InputHandler para evitar movimientos durante la animación
        inputHandler.enabled = false;

        var movements = d2048.GetMovements();
        Transform canvasTransform = board.parent;  // Acceder al padre de board, que es el Canvas
        Vector2 tileSize = tileCreator.GetTileSize();  // Obtener las dimensiones de las casillas

        foreach (var movement in movements)
        {
            Vector2Int startPos = movement.Start;
            Vector2Int endPos = movement.End;
            int value = d2048.board[endPos.x, endPos.y];
            Color originalColor = tileCreator.GetTileColor(startPos);

            // Instanciar una casilla copia como hijo del Canvas con el tamaño y color adecuados
            Vector3 startWorldPos = tileCreator.GetWorldPosition(startPos);
            GameObject tileCopy = tileCreator.CreateTileInstance(startWorldPos, value, canvasTransform, tileSize, originalColor);

            // Poner la casilla original a 0 y en color blanco
            boardRenderer.SetTileValue(startPos, 0);
            boardRenderer.SetTileColor(startPos, 0);

            // Lerpear la copia a la posición final
            yield return StartCoroutine(LerpTile(tileCopy, startPos, endPos));

            // Poner la casilla de la posición final al valor que tenga la copia
            boardRenderer.SetTileValue(endPos, value);

            // Destruir la instancia copia
            Destroy(tileCopy);
        }

        boardRenderer.RenderBoard();
        score.UpdateScore();

        // Reactivar el InputHandler después de la animación
        inputHandler.enabled = true;
    }

    private IEnumerator LerpTile(GameObject tile, Vector2Int startPos, Vector2Int endPos)
    {
        Vector3 start = tileCreator.GetWorldPosition(startPos);
        Vector3 end = tileCreator.GetWorldPosition(endPos);
        float duration = 0.3f; // Duración de la animación
        float elapsed = 0f;

        while (elapsed < duration)
        {
            tile.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        tile.transform.position = end;
    }
}
