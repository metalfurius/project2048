using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform board;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private TextMeshProUGUI scoreText;

    private D2048 d2048;
    private TileColors tileColors;
    private InputHandler inputHandler;

    public void Setup(D2048 d2048)
    {
        this.d2048 = d2048;
        this.tileColors = new TileColors();
        this.inputHandler = GetComponent<InputHandler>();
        StartBoard(d2048);
        board.GetComponent<GridLayoutGroup>().constraintCount = d2048.board.GetLength(0);

        if (inputHandler != null)
        {
            inputHandler.OnMove += MoveBoard;
        }
    }

    private void StartBoard(D2048 d2048)
    {
        for (int i = 0; i < d2048.board.GetLength(0); i++)
        {
            for (int j = 0; j < d2048.board.GetLength(1); j++)
            {
                CreateTile();
            }
        }
        UpdateBoard();
    }

    private void CreateTile()
    {
        GameObject newTile = Instantiate(tilePrefab, Vector2.zero, Quaternion.identity);
        newTile.transform.SetParent(board, false);
    }

    private void UpdateBoard()
    {
        for (int i = 0; i < d2048.board.GetLength(0); i++)
        {
            for (int j = 0; j < d2048.board.GetLength(1); j++)
            {
                int _index = i * d2048.board.GetLength(1) + j;
                Transform _child = board.GetChild(_index);
                int tileValue = d2048.board[i, j];
                _child.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = tileValue > 0 ? tileValue.ToString() : "";
                SetTileColor(_child, tileValue);
            }
        }
        UpdateScore();
    }
    private void MoveBoard(Vector2Int direction)
    {
        d2048.MoveTiles(direction);
        UpdateBoard();
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + d2048.score.ToString();
        }
    }

    private void SetTileColor(Transform tile, int value)
    {
        Image tileImage = tile.GetComponentInChildren<Image>();
        if (tileImage == null)
            return;

        tileImage.color = tileColors.GetColor(value);
    }
}
