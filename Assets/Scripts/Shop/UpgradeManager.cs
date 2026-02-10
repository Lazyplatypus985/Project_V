using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static List<ShopUpgradeScriptableObject> purchasedUpgrades = new List<ShopUpgradeScriptableObject>();

    public static void AddUpgrade(ShopUpgradeScriptableObject upgrade)
    {
        purchasedUpgrades.Add(upgrade);
    }

    public static void ApplyAllUpgrades(PlayerStats player)
    {
        foreach (var upgrade in purchasedUpgrades)
        {
            player.ApplyUpgrade(upgrade);
        }
    }
}

