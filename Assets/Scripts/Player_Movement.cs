using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Player stats from Stats manager
    public Stats_Manager Sta;

    //Movement variables
    private float moveSpeed = 10;

    Rigidbody2D rb;

    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Sta.P_Speed;
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2 (moveDirection.x *  moveSpeed, moveDirection.y * moveSpeed);
    }
}
