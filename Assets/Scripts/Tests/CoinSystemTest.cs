using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CoinSystemTest
{
    [Test]
    public void CheckNegativeValue()
    {
        var coinSystem = new CoinSystem();
        
        Assert.That(coinSystem, Is.Not.Null);
        
        coinSystem.AddCoins(-1000);
       
        Assert.IsTrue(coinSystem.Coins > 0);
    }
    
    [Test]
    public void CheckPositiveValue()
    {
        var coinSystem = new CoinSystem();
      
        Assert.That(coinSystem, Is.Not.Null);
        
        coinSystem.AddCoins(1000);
       
        Assert.IsTrue(coinSystem.Coins >= 1000);
    }
    
    [Test]
    public void ReduceCoins()
    {
        var coinSystem = new CoinSystem();
   
        coinSystem.ReduceCoins(15000);
        Assert.That(coinSystem, Is.Not.Null);
      
        Assert.IsTrue(coinSystem.Coins >= 0);
    }
}
