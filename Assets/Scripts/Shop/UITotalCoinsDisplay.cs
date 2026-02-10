using UnityEngine;
using TMPro;

public class UITotalCoinsDisplay : MonoBehaviour
{
    TextMeshProUGUI display;

    void Start()
    {
        Savemanager.Load();
        display = GetComponent<TextMeshProUGUI>();

        float coins = Savemanager.lastLoadedGameData.coins;

        display.text = Mathf.RoundToInt(coins).ToString();
        display.text = coins.ToString();

    }
}
