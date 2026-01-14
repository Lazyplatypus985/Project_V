using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform player;


    void Start()
    {
        player = FindObjectOfType<Player_Movement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime); // move enemy towards player at all times
    }
}
