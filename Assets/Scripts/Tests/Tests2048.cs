using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Tests2048
{
    [Test]
    public void TestScoreDefault()
    {
        var _game = new D2048(new Vector2Int(1,1));
        Assert.AreEqual(0, _game.Score);
    }
    [Test]
    public void TestScoreAfterMerge()
    {
        var _game = new D2048(new Vector2Int(1, 1));
        _game.AddScore(8);
        Assert.AreEqual(8, _game.Score);
    }
    [Test]
    public void TestCreateBoard()
    {
        var _game = new D2048(new Vector2Int(1, 1));
        Assert.IsNotNull(_game.Board);
    }
    [Test]
    public void TestGenerateNewTile()
    {
        var _game = new D2048(new Vector2Int(4, 4));
        _game.GenerateNewTile();
        Assert.IsTrue(_game.NumberedTiles>0);
    }

    [Test]
    public void TestMoveTilesUp()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [1, 0] = 2
            }
        };

        _game.MoveTiles(D2048.Up);

        Assert.AreEqual(2, _game.Board[0, 0]);
    }
    [Test]
    public void TestMoveUpTileCombine()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [0, 0] = 2,
                [1, 0] = 2
            }
        };

        _game.MoveTiles(D2048.Up);

        Assert.AreEqual(4, _game.Board[0, 0]);
    }
    [Test]
    public void TestMoveUpTileCleanup()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [0, 0] = 2,
                [1, 0] = 2
            }
        };

        _game.MoveTiles(D2048.Up);

        Assert.AreEqual(0, _game.Board[1, 0]);
    }

    [Test]
    public void TestMoveUpTilesAllColumnsAndCleanup()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [0, 0] = 2,
                [1, 0] = 2,
                [1, 1] = 2,
                [2, 1] = 2,
                [0, 2] = 2,
                [3, 2] = 2,
                [0, 3] = 2,
                [2, 3] = 2
            }
        };

        _game.MoveTiles(D2048.Up);

        Assert.AreEqual(4, _game.Board[0, 0]);
        Assert.AreEqual(0, _game.Board[1, 0]);

        Assert.AreEqual(4, _game.Board[0, 1]);
        Assert.AreEqual(0, _game.Board[1, 1]);

        Assert.AreEqual(4, _game.Board[0, 2]);
        Assert.AreEqual(0, _game.Board[3, 2]);

        Assert.AreEqual(4, _game.Board[0, 3]);
        Assert.AreEqual(0, _game.Board[2, 3]);
    }
    [Test]
    public void TestMoveTilesRight()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [1, 0] = 2
            }
        };

        _game.MoveTiles(D2048.Right);

        Assert.AreEqual(2, _game.Board[1, 3]);
    }
    [Test]
    public void TestMoveUpTileNotCombine()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [0, 0] = 2,
                [1, 0] = 4
            }
        };

        _game.MoveTiles(D2048.Up);

        Assert.AreEqual(2, _game.Board[0, 0]);
    }
    [Test]
    public void TestMoveTilesDown()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [1, 0] = 2
            }
        };

        _game.MoveTiles(D2048.Down);

        Assert.AreEqual(2, _game.Board[3, 0]);
    }
    [Test]
    public void TestMoveTilesLeft()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [1, 3] = 2
            }
        };

        _game.MoveTiles(D2048.Left);

        Assert.AreEqual(2, _game.Board[1, 0]);
    }
    [Test]
    public void TestSourceDestinationMovements()
    {
        var _game = new D2048(new Vector2Int(4, 4));
        var _movementsTest = new Movement(new Vector2Int(1, 3), new Vector2Int(1, 0));

        _game.Board[1, 3] = 2;
        _game.MoveTiles(D2048.Left);

        var _movements = _game.GetMovements();
        Assert.AreEqual(_movementsTest.Start, _movements[0].Start);
        Assert.AreEqual(_movementsTest.End, _movements[0].End);
    }
    [Test]
    public void TestMultipleSourceDestinationMovements()
    {
        var _game = new D2048(new Vector2Int(4, 4))
        {
            Board =
            {
                [1, 3] = 2,
                [2, 3] = 2,
                [3, 3] = 4
            }
        };

        var _expectedMovements = new List<Movement>
        {
        new Movement(new Vector2Int(1, 3), new Vector2Int(1, 0)),
        new Movement(new Vector2Int(2, 3), new Vector2Int(2, 0)),
        new Movement(new Vector2Int(3, 3), new Vector2Int(3, 0))
        };

        _game.MoveTiles(D2048.Left);

        var _movements = _game.GetMovements();

        for (var _i = 0; _i < _expectedMovements.Count; _i++)
        {
            Assert.AreEqual(_expectedMovements[_i].Start, _movements[_i].Start);
            Assert.AreEqual(_expectedMovements[_i].End, _movements[_i].End);
        }
    }

}
