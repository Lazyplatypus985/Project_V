using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    Enemy_Stats enemyStats;
    Transform player;


    void Start()
    {
        player = FindObjectOfType<Player_Movement>().transform;
        enemyStats = GetComponent<Enemy_Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.currentMoveSpeed * Time.deltaTime); // move enemy towards player at all times
    }
}
