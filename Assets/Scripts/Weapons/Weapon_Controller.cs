using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float current_Cooldown;

    protected Player_Movement pm;
    protected virtual void Start()
    {
        pm  = FindObjectOfType<Player_Movement>();
        current_Cooldown = weaponData.CooldownDuration;
    }

    protected virtual void Update()
    {
        current_Cooldown -= Time.deltaTime;
        if (current_Cooldown <= 0f)
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        current_Cooldown = weaponData.CooldownDuration;
    }
}
