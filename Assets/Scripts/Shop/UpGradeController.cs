using UnityEngine;
using UnityEngine.UI;

public class UpGradeController : MonoBehaviour
{
    private float currentLevel = 0;
    public ShopUpgradeScriptableObject data;
    float Coins;
    public Text nameText;
    public Text levelText;
    public Text costText;
    public Image iconImage;

    void Start()
    {
        Savemanager.Load();
        LoadLevel();
        SetupUI();
    }

    void SetupUI()
    {
        nameText.text = data.Name;
        iconImage.sprite = data.Icon;
        UpdateUI();
    }

    void UpdateUI()
    {
        Debug.Log(currentLevel);
        levelText.text = "LVL: " + currentLevel + "/" + data.MaxLVL;
        Debug.Log(levelText.text);
        costText.text = "Cost: " + GetCost();
    }

    public void BuyUpgrade()
    {
        float coins = Savemanager.lastLoadedGameData.coins;
        if (currentLevel >= data.MaxLVL) 
            return; int cost = GetCost(); 
        if (coins >= cost) 
        {
            Savemanager.lastLoadedGameData.coins -= cost; currentLevel++;
            SaveLevel(); 
            Savemanager.Save(); 
            UpdateUI();
        }
    }

    int GetCost()
    {
        return (int)(data.BaseCost * (currentLevel + 1));
    }
    void LoadLevel()
    {
        foreach (var upg in Savemanager.lastLoadedGameData.upgradeLevels)
        {
            if (upg.upgradeName == data.Name)
            {
                currentLevel = upg.level;
                return;
            }
        }

        // If not found, create a new entry
        Savemanager.lastLoadedGameData.upgradeLevels.Add(new Savemanager.UpgradeLevelData
        {
            upgradeName = data.Name,
            level = 0
        });

        Savemanager.Save();
    }
    void SaveLevel()
    {
        foreach (var upg in Savemanager.lastLoadedGameData.upgradeLevels)
        {
            if (upg.upgradeName == data.Name)
            {
                upg.level = currentLevel;
                return;
            }
        }
    }


}
