using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Weapon_Controller> weaponSlots = new List<Weapon_Controller>(6);
    public int[] passiveWeaponLevels = new int[6];

    public List<Passive_Item> itemSlots = new List<Passive_Item>(6);
    public int[] passiveItemLevels = new int[6];

    public void AddWeapon(int slotIndex, Weapon_Controller weapon)
    {
        weaponSlots[slotIndex] = weapon;
    }

    public void AddPassiveItem(int slotIndex, Passive_Item passiveItem)
    {
        itemSlots[slotIndex] = passiveItem;
    }

    public void LevelUpWeapon(int slotIndex) 
    {
    
    }

    public void LevelUpPassiveItem(int slotIndex) 
    {
    
    }
}
