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
        Assert.IsTrue(test.numberedCells>0);
    }

    [Test]
    public void TestMoveTilesUp()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[0, 0] = 2;
        game.board[1, 0] = 2;

        game.MoveTiles(D2048.Up);

        Assert.AreEqual(4, game.board[0, 0]);
        Assert.AreEqual(0, game.board[1, 0]);
    }
}
