using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade_Behaviour : Melee_Weapon_Behaviour
{

    public float spinSpeed = 720f;
    List<GameObject> markedEnemies;
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !markedEnemies.Contains(collision.gameObject))
        {
            Enemy_Stats enemy = collision.GetComponent<Enemy_Stats>();
            enemy.TakeDamage(GetCurrentDamage());

            markedEnemies.Add(collision.gameObject);
        }
    }
}