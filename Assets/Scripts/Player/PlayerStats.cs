using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.WSA.Input;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;

    //Current stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;



    #region currentstats

    public float CurrentHealth 
    {  
        get { return currentHealth; } 
        set 
        {
            if (currentHealth != value) 
            {
                currentHealth = value;
                if (GameManager.instance != null) 
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: "+ currentHealth;
                }
            }
        } 
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Speed: " + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set 
        {
                if (currentProjectileSpeed != value)
                {
                    currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "ProjectileSpeed: " + currentProjectileSpeed;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }
    #endregion

    // exp method
    [Header("exp/lvl")]
    public int exp = 0;
    public int level = 1;
    public int expCap;

    [System.Serializable]
    public class LevelRange 
    {
        public int startLevel;
        public int endLevel;
        public int expCapIncrease;
    }

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public Text levelText;

    public GameObject second_Weapontest;
    public GameObject itemtest1;
    public GameObject itemtest2;

    void Awake()
    {
        playerData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        CurrentHealth = playerData.MaxHealth;
        CurrentRecovery = playerData.Recovery;
        CurrentMoveSpeed = playerData.MoveSpeed;
        CurrentMight = playerData.Might;
        CurrentProjectileSpeed = playerData.ProjectileSpeed;
        CurrentMagnet = playerData.Magnet;



        SpawnWeapon(playerData.StartingWeapon);
        //SpawnWeapon(second_Weapontest);
        //SpawnPassiveItem(itemtest1);
        SpawnPassiveItem(itemtest2);
    }


    void Start() 
    {
        expCap = levelRanges[0].expCapIncrease;

        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "ProjectileSpeed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;

        GameManager.instance.AssignChosenCharacterUI(playerData);
        UpdateHPBar();
        UpdateExpBar();
        UpdateExpText();
    }
    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible) 
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExp(int amount) 
    {
        exp += amount;
        LevelUpChecker();
        UpdateExpBar();
    }

    void LevelUpChecker()
    {
        if (exp >= expCap) 
        {
            level++;
            exp -= expCap;

            int expCapIncrease = 0;
            foreach (LevelRange range in levelRanges) 
            { 
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    expCapIncrease = range.expCapIncrease;
                    break;
                }
            }
            expCap += expCapIncrease;

            UpdateExpText();
            

            GameManager.instance.StartLevelUp();
        }
    }

    void UpdateExpBar()
    {
        expBar.fillAmount = (float) exp / expCap;
    }

    void UpdateExpText()
    {
        levelText.text = "LVL " + level.ToString();
    }


    public void TakeDamage(float dmg)
    {
        if (!isInvincible) 
        {
            CurrentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (CurrentHealth < 0)
            {
                Kill();
            }
            UpdateHPBar();
        }

    }
    void UpdateHPBar()
    {
        healthBar.fillAmount = currentHealth / playerData.MaxHealth;
    }

    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssingLevelReachedUI(level);
            GameManager.instance.AssingChosenItemsAndWeaponsUI(inventory.weaponUiSlots, inventory.itemUiSlots);
            GameManager.instance.GameOver();
        }
    }

    void Recover()
    {
        if (CurrentHealth < playerData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            if (CurrentHealth > playerData.MaxHealth) 
            {
                CurrentHealth = playerData.MaxHealth;
            }

        }

    }

    public void SpawnWeapon(GameObject weapon)
    {
        if(weaponIndex >= inventory.weaponSlots.Count -1)
        {
            Debug.LogError("Inventoryt full");
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<Weapon_Controller>());

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.itemSlots.Count - 1)
        {
            Debug.LogError("Inventoryt full");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<Passive_Item>());

        passiveItemIndex++;
    }
}
