using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Weapon_Behaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
   
    protected Vector3 direction;
    public float destroyAfterSeconds;

    // current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().currentMight;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;
        if (dirx < 0 && diry == 0) 
        {
            scale.x = Mathf.Abs(scale.x);   // normal X
            scale.y = Mathf.Abs(scale.y);
            rotation.z = 0f;                
        }
        else if (dirx > 0 && diry == 0) // right
        {
            scale.x = -Mathf.Abs(scale.x);  // flip X to face right
            scale.y = Mathf.Abs(scale.y);
            rotation.z = 0f;           
        }
        else if (dirx == 0 && diry > 0) // up
        {
            scale.x = Mathf.Abs(scale.x);
            scale.y = Mathf.Abs(scale.y);
            rotation.z = -90f;          
        }
        else if (dirx == 0 && diry < 0) // down
        {
            scale.x = Mathf.Abs(scale.x);
            scale.y = Mathf.Abs(scale.y);
            rotation.z = 90f;              
        }
        else if (dirx > 0 && diry > 0) // up-right
        {
            scale.x = -Mathf.Abs(scale.x); 
            scale.y = Mathf.Abs(scale.y);
            rotation.z = 45f;
        }
        else if (dirx > 0 && diry < 0) // down-right
        {
            scale.x = -Mathf.Abs(scale.x);  
            scale.y = Mathf.Abs(scale.y);
            rotation.z = -45f;
        }
        else if (dirx < 0 && diry > 0) // up-left
        {
            scale.x = Mathf.Abs(scale.x);
            scale.y = Mathf.Abs(scale.y);
            rotation.z = -45f;
        }
        else if (dirx < 0 && diry < 0) // down-left
        {
            scale.x = Mathf.Abs(scale.x);
            scale.y = Mathf.Abs(scale.y);
            rotation.z = 45f;
        }


        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy_Stats enemy = col.GetComponent<Enemy_Stats>();
            enemy.TakeDamage(GetCurrentDamage());
            ReducePierce();
        }
    }
    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
