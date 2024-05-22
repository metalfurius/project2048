using System;
using System.Collections;
using System.Collections.Generic;
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
        for (int i = d2048.board.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < d2048.board.GetLength(1); j++)
                CreateTile();
        }
    }

    private void CreateTile()
    {
        GameObject newTile = Instantiate(tilePrefab,Vector2.zero,Quaternion.identity);
        newTile.transform.SetParent(board);
    }
}
