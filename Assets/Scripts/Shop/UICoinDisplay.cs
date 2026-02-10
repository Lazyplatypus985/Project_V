using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICoinDisplay : MonoBehaviour
{
    TextMeshProUGUI displaytarget;
    public Player_Collector collector;
    void Start()
    {
        displaytarget = GetComponent<TextMeshProUGUI>();
        Update_Display();
        if (collector != null) 
        {
            collector.onCoinCollected += Update_Display;
        }

    }

    public void Update_Display()
    {
        if (collector != null)
        {
            displaytarget.text = Mathf.RoundToInt(collector.GetCoins()).ToString();
        }
        else 
        {
            float coins = Savemanager.lastLoadedGameData.coins;
            displaytarget.text = Mathf.RoundToInt(coins).ToString();
        }
    }
}
