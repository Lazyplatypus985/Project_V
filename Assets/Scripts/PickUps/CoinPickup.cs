using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : PickUp, ICollectable
{
    public int coins = 1;

    public void Collect()
    {
        Player_Collector collector = FindObjectOfType<Player_Collector>();
        if (collector != null) 
        { 
            collector.AddCoins(coins);
        }
    }

}

