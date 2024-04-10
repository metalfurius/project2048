using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Tests2048
{
    [Test]
    public void TestDefault()
    {
        D2048 test=new D2048(0);
        Assert.AreEqual(0, test.score);
    }
}
