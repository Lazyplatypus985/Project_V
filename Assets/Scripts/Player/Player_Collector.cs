using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collector : MonoBehaviour
{
    PlayerStats playerStats;
    CircleCollider2D playerCollector;
    public float pullSpeed;

    public delegate void OnCoinCollected();
    public OnCoinCollected onCoinCollected;

    float coins;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
        coins = 0;
    }
    void Update()
    {
        playerCollector.radius = playerStats.CurrentMagnet;
    }

    public float GetCoins() 
    {
        return coins;
    }
    
    public float AddCoins(float amount)
    {
        coins += amount;
        onCoinCollected?.Invoke();
        return coins;
    }


    public void SaveCoinsToStash(float coins) 
    {
        Debug.Log("[Save] Saving coins: " + coins); 
        Debug.Log("[Save] Before save file coins: " + Savemanager.lastLoadedGameData.coins);
        Savemanager.lastLoadedGameData.coins += coins;
        Debug.Log("[Save] After save file coins: " + Savemanager.lastLoadedGameData.coins);
        coins = 0;
        Savemanager.Save();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ICollectable collectable))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            rb.AddForce(forceDirection * pullSpeed);

            collectable.Collect();
        }
    }
}
