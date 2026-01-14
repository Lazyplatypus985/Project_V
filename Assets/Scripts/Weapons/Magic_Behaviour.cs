using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Behaviour : Projectile_Weapon_Behaviour
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponData == null)
        {
            Debug.LogError("weaponData is NULL on " + gameObject.name);
            return;
        }
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}
