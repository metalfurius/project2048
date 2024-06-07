using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform board;
    [SerializeField] private GameObject tilePrefab;
    D2048 d2048;

    public void Setup(D2048 d2048)
    {
        this.d2048 = d2048;
        StartBoard(d2048);
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
            }
        }
    }
    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveBoard(D2048.Up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveBoard(D2048.Down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveBoard(D2048.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveBoard(D2048.Right);
        }
    }

    private void MoveBoard(Vector2Int direction)
    {
        d2048.MoveTiles(direction);
        UpdateBoard();
    }
}
