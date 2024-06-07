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
    D2048 d2048;

    [SerializeField] private TextMeshProUGUI scoreText;  
    private Dictionary<int, Color> tileColors;

    public void Setup(D2048 d2048)
    {
        InitializeTileColors();
        this.d2048 = d2048;
        StartBoard(d2048);
        board.GetComponent<GridLayoutGroup>().constraintCount = d2048.board.GetLength(0);
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
        if (tileImage != null)
        {
            if (tileColors.TryGetValue(value, out Color color))
            {
                tileImage.color = color;
            }
            else
            {
                tileImage.color = Color.white; // Color por defecto para valores no definidos
            }
        }
    }
    private void InitializeTileColors()
    {
        tileColors = new Dictionary<int, Color>
        {
            { 2, new Color(0.93f, 0.89f, 0.85f) }, // Light beige
            { 4, new Color(0.93f, 0.88f, 0.78f) }, // Light brown
            { 8, new Color(0.95f, 0.69f, 0.47f) }, // Light orange
            { 16, new Color(0.96f, 0.58f, 0.39f) }, // Darker orange
            { 32, new Color(0.96f, 0.49f, 0.37f) }, // Dark orange
            { 64, new Color(0.96f, 0.37f, 0.23f) }, // Red orange
            { 128, new Color(0.93f, 0.81f, 0.44f) }, // Gold
            { 256, new Color(0.78f, 0.63f, 0.20f) }, // Dark gold
            { 512, new Color(0.69f, 0.55f, 0.15f) }, // Darker gold
            { 1024, new Color(0.60f, 0.49f, 0.13f) }, // Even darker gold
            { 2048, new Color(0.49f, 0.38f, 0.10f) }  // Dark brown
        };
    }
}
