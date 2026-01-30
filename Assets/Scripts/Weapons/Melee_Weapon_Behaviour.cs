using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Weapon_Behaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyAftherSeconds;


    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldown;
    protected int current_pierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldown = weaponData.CooldownDuration;
        current_pierce = weaponData.Pierce;
    }
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAftherSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy_Stats enemy = collision.GetComponent<Enemy_Stats>();
            enemy.TakeDamage(GetCurrentDamage());
        }
    }
}
