using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardAnimator
{
    private readonly Board board;
    private readonly InputHandler inputHandler;
    private readonly TileCreator tileCreator;
    private readonly BoardRenderer boardRenderer;
    private readonly Score score;
    private readonly float totalAnimationDuration;

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

        var _canvasTransform = GetCanvasTransform();
        var _tileSize = GetTileSize();
        var _animationCoroutines = StartTileAnimations(movements, _canvasTransform, _tileSize);

        yield return WaitForCoroutinesToFinish(_animationCoroutines);

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
        return movements.Select(movement => StartTileAnimation(movement, canvasTransform, tileSize)).ToList();
    }

    private Coroutine StartTileAnimation(Movement movement, Transform canvasTransform, Vector2 tileSize)
    {
        var _startPos = movement.Start;
        var _endPos = movement.End;
        var _value = board.GetTileValue(_endPos);
        var _originalColor = tileCreator.GetTileColor(_startPos);

        var _startWorldPos = tileCreator.GetWorldPosition(_startPos);
        var _tileCopy = tileCreator.CreateTileInstance(_startWorldPos, _value, canvasTransform, tileSize, _originalColor);

        ClearTileAtStartPosition(_startPos);

        return board.StartCoroutine(MoveTile(_tileCopy, _endPos, totalAnimationDuration, () =>
        {
            SetTileAtEndPosition(_endPos, _value, movement.IsMerge, _tileCopy);
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
        return coroutines.GetEnumerator();
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
        var _end = tileCreator.GetWorldPosition(endPos);
        Tween _tween = tile.transform.DOMove(_end, duration).SetEase(Ease.InOutCirc);

        yield return _tween.WaitForCompletion();
        onComplete?.Invoke();
    }
}
