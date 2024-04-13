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
}
