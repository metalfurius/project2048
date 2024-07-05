using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAnimator
{
    private Board board;
    private InputHandler inputHandler;
    private TileCreator tileCreator;
    private BoardRenderer boardRenderer;
    private Score score;
    private float totalAnimationDuration;

    public BoardAnimator(Board board, InputHandler inputHandler, TileCreator tileCreator, BoardRenderer boardRenderer, Score score, float totalAnimationDuration)
    {
        this.board = board;
        this.inputHandler = inputHandler;
        this.tileCreator = tileCreator;
        this.boardRenderer = boardRenderer;
        this.score = score;
        this.totalAnimationDuration = totalAnimationDuration;
    }

    public IEnumerator AnimateMovements(List<Movement> movements)
    {
        inputHandler.enabled = false;

        Transform canvasTransform = Object.FindAnyObjectByType<Canvas>().transform;
        Vector2 tileSize = tileCreator.GetTileSize();

        List<Coroutine> animationCoroutines = new List<Coroutine>();

        foreach (var movement in movements)
        {
            Vector2Int startPos = movement.Start;
            Vector2Int endPos = movement.End;
            int value = board.GetTileValue(endPos);
            Color originalColor = tileCreator.GetTileColor(startPos);

            Vector3 startWorldPos = tileCreator.GetWorldPosition(startPos);
            GameObject tileCopy = tileCreator.CreateTileInstance(startWorldPos, value, canvasTransform, tileSize, originalColor);

            boardRenderer.SetTileValue(startPos, 0);
            boardRenderer.SetTileColor(startPos, 0);

            Coroutine animation = board.StartCoroutine(DOTweenTile(tileCopy, endPos, totalAnimationDuration, () =>
            {
                boardRenderer.SetTileValue(endPos, value);

                if (movement.IsMerge)
                {
                    boardRenderer.SetTileColor(endPos, value);
                }

                Object.Destroy(tileCopy);
            }));
            animationCoroutines.Add(animation);
        }

        foreach (var coroutine in animationCoroutines)
        {
            yield return coroutine;
        }

        boardRenderer.RenderBoard();
        score.UpdateScore();

        inputHandler.enabled = true;
    }

    private IEnumerator DOTweenTile(GameObject tile, Vector2Int endPos, float duration, System.Action onComplete)
    {
        Vector3 end = tileCreator.GetWorldPosition(endPos);

        Tween tween = tile.transform.DOMove(end, duration).SetEase(Ease.Linear);

        yield return tween.WaitForCompletion();

        onComplete?.Invoke();
    }
}
