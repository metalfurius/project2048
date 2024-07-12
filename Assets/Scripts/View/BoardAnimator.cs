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
        DisableInputHandler();

        Transform canvasTransform = GetCanvasTransform();
        Vector2 tileSize = GetTileSize();
        List<Coroutine> animationCoroutines = StartTileAnimations(movements, canvasTransform, tileSize);

        yield return WaitForCoroutinesToFinish(animationCoroutines);

        FinalizeAnimation();
    }

    private void DisableInputHandler()
    {
        inputHandler.enabled = false;
    }

    private Transform GetCanvasTransform()
    {
        return Object.FindAnyObjectByType<Canvas>().transform;
    }

    private Vector2 GetTileSize()
    {
        return tileCreator.GetTileSize();
    }

    private List<Coroutine> StartTileAnimations(List<Movement> movements, Transform canvasTransform, Vector2 tileSize)
    {
        List<Coroutine> animationCoroutines = new List<Coroutine>();

        foreach (var movement in movements)
        {
            Coroutine animation = StartTileAnimation(movement, canvasTransform, tileSize);
            animationCoroutines.Add(animation);
        }

        return animationCoroutines;
    }

    private Coroutine StartTileAnimation(Movement movement, Transform canvasTransform, Vector2 tileSize)
    {
        Vector2Int startPos = movement.Start;
        Vector2Int endPos = movement.End;
        int value = board.GetTileValue(endPos);
        Color originalColor = tileCreator.GetTileColor(startPos);

        Vector3 startWorldPos = tileCreator.GetWorldPosition(startPos);
        GameObject tileCopy = tileCreator.CreateTileInstance(startWorldPos, value, canvasTransform, tileSize, originalColor);

        ClearTileAtStartPosition(startPos);

        return board.StartCoroutine(MoveTile(tileCopy, endPos, totalAnimationDuration, () =>
        {
            SetTileAtEndPosition(endPos, value, movement.IsMerge, tileCopy);
        }));
    }

    private void ClearTileAtStartPosition(Vector2Int startPos)
    {
        boardRenderer.SetTileValue(startPos, 0);
        boardRenderer.SetTileColor(startPos, 0);
    }

    private void SetTileAtEndPosition(Vector2Int endPos, int value, bool isMerge, GameObject tileCopy)
    {
        boardRenderer.SetTileValue(endPos, value);

        if (isMerge)
        {
            boardRenderer.SetTileColor(endPos, value);
        }

        Object.Destroy(tileCopy);
    }

    private IEnumerator WaitForCoroutinesToFinish(List<Coroutine> coroutines)
    {
        foreach (var coroutine in coroutines)
        {
            yield return coroutine;
        }
    }

    private void FinalizeAnimation()
    {
        boardRenderer.RenderBoard();
        score.UpdateScore();
        EnableInputHandler();
    }

    private void EnableInputHandler()
    {
        inputHandler.enabled = true;
    }

    private IEnumerator MoveTile(GameObject tile, Vector2Int endPos, float duration, System.Action onComplete)
    {
        Vector3 end = tileCreator.GetWorldPosition(endPos);
        Tween tween = tile.transform.DOMove(end, duration).SetEase(Ease.InOutCirc);

        yield return tween.WaitForCompletion();
        onComplete?.Invoke();
    }
}
