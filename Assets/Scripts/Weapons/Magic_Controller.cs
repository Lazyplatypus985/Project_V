using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Controller : Weapon_Controller
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBullet = Instantiate(weaponData. Prefab);
        spawnedBullet.transform.position = transform.position;
        spawnedBullet.GetComponent<Magic_Behaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
