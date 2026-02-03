using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Weapon_Controller> weaponSlots = new List<Weapon_Controller>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUiSlots = new List<Image>(6);
    public List<Passive_Item> itemSlots = new List<Passive_Item>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> itemUiSlots = new List<Image>(6);

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public WeaponScriptableObject weaponData;
    }

    [System.Serializable]
    public class ItemUpgrade
    {
        public int itemUpgradeIndex;
        public GameObject initialItem;
        public Passive_Item_ScriptableObject itemData;
    }

    [System.Serializable ]
    public class UpgradeUI
    {
        public Text upgradeNameDisplay;
        public Text upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<ItemUpgrade> itemUpgradeOptions = new List<ItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    PlayerStats player;

    void Start()
    {

        player = GetComponent<PlayerStats>();
    }

    public void AddWeapon(int slotIndex, Weapon_Controller weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUiSlots[slotIndex].enabled = true;
        weaponUiSlots[slotIndex].sprite = weapon.weaponData.Icon;

        if (GameManager.instance != null && GameManager.instance.choosingLevelUp)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void AddPassiveItem(int slotIndex, Passive_Item passiveItem)
    {
        itemSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.PassiveItemData.Level;
        itemUiSlots[slotIndex].enabled = true;
        itemUiSlots[slotIndex].sprite = passiveItem.PassiveItemData.Icon;
        if (GameManager.instance != null && GameManager.instance.choosingLevelUp)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex) 
    {
        if (weaponSlots.Count > slotIndex) 
        {
            Weapon_Controller weapon = weaponSlots[slotIndex];
            if (!weapon.weaponData.NextLevelPrefab) 
            {
                Debug.LogError("no next level for " + weapon.name);
                return;
            }
            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<Weapon_Controller>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex ] = upgradedWeapon.GetComponent<Weapon_Controller>().weaponData.Level;

            weaponUpgradeOptions[upgradeIndex].weaponData = upgradedWeapon.GetComponent<Weapon_Controller>().weaponData;

            if (GameManager.instance != null && GameManager.instance.choosingLevelUp)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    public void LevelUpPassiveItem(int slotIndex, int upgradeIndex) 
    {
        if (itemSlots.Count > slotIndex)
        {
            Passive_Item passiveItem = itemSlots[slotIndex];
            if (!passiveItem.PassiveItemData.NextLevelPrefab)
            {
                
                Debug.LogError("no next level for " + passiveItem.name);
            }
            GameObject upgradedPassiveItem = Instantiate(passiveItem.PassiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<Passive_Item>());
            Destroy(passiveItem.gameObject);
            weaponLevels[slotIndex] = upgradedPassiveItem.GetComponent<Passive_Item>().PassiveItemData.Level;

            itemUpgradeOptions[upgradeIndex].itemData = upgradedPassiveItem.GetComponent <Passive_Item>().PassiveItemData;

            if (GameManager.instance != null && GameManager.instance.choosingLevelUp)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }
    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availableWeaponUpgrade = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<ItemUpgrade> availableItemUpgrade = new List<ItemUpgrade>(itemUpgradeOptions);

        foreach (var upgradeOption in upgradeUIOptions)
        {
            if (availableWeaponUpgrade.Count == 0 && availableItemUpgrade.Count == 0)
            {
                return;
            }

            int upgradeType;

            if (availableWeaponUpgrade.Count == 0) 
            {
                upgradeType = 2;
            }

            else if (availableItemUpgrade.Count == 0) 
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3);
            }

            if (upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrade[Random.Range(0, availableWeaponUpgrade.Count)];

                availableWeaponUpgrade.Remove(chosenWeaponUpgrade);


                if (chosenWeaponUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);

                    bool newWeapon = false;
                    for (int i = 0; i < weaponSlots.Count; i++)
                    {
                        if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }

                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex));
                                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<Weapon_Controller>().weaponData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<Weapon_Controller>().weaponData.Name;
                                upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<Weapon_Controller>().weaponData.Icon;

                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }
                    if (newWeapon)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));
                        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.name;
                        upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
                    }
                }
            }

            else if (upgradeType == 2)
            {
                ItemUpgrade chosenItemUpgrade = availableItemUpgrade[Random.Range(0, availableItemUpgrade.Count)];

                availableItemUpgrade.Remove(chosenItemUpgrade);

                if (chosenItemUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);
                    bool newItem = false;
                    for (int i = 0; i < itemSlots.Count; i++)
                    {
                        if (itemSlots[i] != null && itemSlots[i].PassiveItemData == chosenItemUpgrade.itemData)
                        {
                            newItem = false;

                            if (!newItem)
                            {
                                if (!chosenItemUpgrade.itemData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }

                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i, chosenItemUpgrade.itemUpgradeIndex));
                                upgradeOption.upgradeDescriptionDisplay.text = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<Passive_Item>().PassiveItemData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<Passive_Item>().PassiveItemData.Name;
                                upgradeOption.upgradeIcon.sprite = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<Passive_Item>().PassiveItemData.Icon;
                            }
                            break;
                        }
                        else
                        {
                            newItem = true;
                        }
                    }
                    if (newItem)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenItemUpgrade.initialItem));
                        upgradeOption.upgradeDescriptionDisplay.text = chosenItemUpgrade.itemData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenItemUpgrade.itemData.name;
                        upgradeOption.upgradeIcon.sprite = chosenItemUpgrade.itemData.Icon;
                    }

                    

                }
            }
        }
    }
    void RemoveUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOption);
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }
    void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }
}
