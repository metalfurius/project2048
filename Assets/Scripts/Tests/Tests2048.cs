using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Tests2048
{
    [Test]
    public void TestScoreDefault()
    {
        D2048 test = new D2048(0, new Vector2Int(1,1));
        Assert.AreEqual(0, test.score);
    }
    [Test]
    public void TestScoreAfterMerge()
    {
        D2048 test = new D2048(0,new Vector2Int(1, 1));
        test.MergeTiles(8);
        Assert.AreEqual(8, test.score);
    }
    [Test]
    public void TestCreateBoard()
    {
        D2048 test = new D2048(0, new Vector2Int(1, 1));
        Assert.IsNotNull(test.board);
    }
    [Test]
    public void TestGenerateNewTile()
    {
        D2048 test = new D2048(0, new Vector2Int(4, 4));
        test.GenerateNewTile();

        bool newTileGenerated = false;
        for (int i = 0; i < test.board.GetLength(0); i++)
        {
            for (int j = 0; j < test.board.GetLength(1); j++)
            {
                if (test.board[i, j] != 0)
                {
                    newTileGenerated = true;
                    break;
                }
            }
            if (newTileGenerated)
            {
                break;
            }
        }
        Assert.IsTrue(newTileGenerated);
    }
}
