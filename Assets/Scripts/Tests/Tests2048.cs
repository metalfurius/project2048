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
        D2048 game = new D2048(0, new Vector2Int(1,1));
        Assert.AreEqual(0, game.score);
    }
    [Test]
    public void TestScoreAfterMerge()
    {
        D2048 game = new D2048(0,new Vector2Int(1, 1));
        game.MergeTiles(8);
        Assert.AreEqual(8, game.score);
    }
    [Test]
    public void TestCreateBoard()
    {
        D2048 game = new D2048(0, new Vector2Int(1, 1));
        Assert.IsNotNull(game.board);
    }
    [Test]
    public void TestGenerateNewTile()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));
        game.GenerateNewTile();
        Assert.IsTrue(game.numberedTiles>0);
    }

    [Test]
    public void TestMoveTilesUp()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[1, 0] = 2;

        game.MoveTiles(D2048.Up);

        Assert.AreEqual(2, game.board[0, 0]);
    }
    [Test]
    public void TestMoveUpTileCombine()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[0, 0] = 2;
        game.board[1, 0] = 2;

        game.MoveTiles(D2048.Up);

        Assert.AreEqual(4, game.board[0, 0]);
    }
    [Test]
    public void TestMoveUpTileCleanup()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[0, 0] = 2;
        game.board[1, 0] = 2;

        game.MoveTiles(D2048.Up);

        Assert.AreEqual(0, game.board[1, 0]);
    }

    [Test]
    public void TestMoveUpTilesAllColumnsAndCleanup()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[0, 0] = 2;
        game.board[1, 0] = 2;

        game.board[1, 1] = 2;
        game.board[2, 1] = 2;

        game.board[0, 2] = 2;
        game.board[3, 2] = 2;

        game.board[0, 3] = 2;
        game.board[2, 3] = 2;

        game.MoveTiles(D2048.Up);

        Assert.AreEqual(4, game.board[0, 0]);
        Assert.AreEqual(0, game.board[1, 0]);

        Assert.AreEqual(4, game.board[0, 1]);
        Assert.AreEqual(0, game.board[1, 1]);

        Assert.AreEqual(4, game.board[0, 2]);
        Assert.AreEqual(0, game.board[3, 2]);

        Assert.AreEqual(4, game.board[0, 3]);
        Assert.AreEqual(0, game.board[2, 3]);
    }
    [Test]
    public void TestMoveTilesRight()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[1, 0] = 2;

        game.MoveTiles(D2048.Right);

        Assert.AreEqual(2, game.board[1, 3]);
    }
    [Test]
    public void TestMoveUpTileNOTCombine()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[0, 0] = 2;
        game.board[1, 0] = 4;

        game.MoveTiles(D2048.Up);

        Assert.AreEqual(2, game.board[0, 0]);
    }
    [Test]
    public void TestMoveTilesDown()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[1, 0] = 2;

        game.MoveTiles(D2048.Down);

        Assert.AreEqual(2, game.board[3, 0]);
    }
    [Test]
    public void TestMoveTilesLeft()
    {
        D2048 game = new D2048(0, new Vector2Int(4, 4));

        game.board[1, 3] = 2;

        game.MoveTiles(D2048.Right);

        Assert.AreEqual(2, game.board[1, 0]);
    }
}
