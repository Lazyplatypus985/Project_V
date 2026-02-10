using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerScriptableObject;

[CreateAssetMenu(fileName = "ShopUpgradeScriptableObject", menuName = "ScriptableObjects/ShopUpgrades")]
public class ShopUpgradeScriptableObject : ScriptableObject
{
    public enum UpgradeTypes
    {
        Health,
        Recovery,
        MoveSpeed,
        Might,
        ProjectileSpeed,
        Magnet
    }
    [SerializeField]
    new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField]
    float statIncrease;
    public float StatIncrease { get => statIncrease; private set => statIncrease = value; }

    [SerializeField]
    float maxLVL;
    public float MaxLVL { get => maxLVL; private set => maxLVL = value; }

    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField]
    float baseCost;
    public float BaseCost { get => baseCost; private set => baseCost = value; }

    [SerializeField]
    UpgradeTypes upgradeType;
    public UpgradeTypes UpgradeType { get => upgradeType; private set => upgradeType = value; }
}
