using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawner;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota; // total number of enemies in a vawe
        public float spawnInterval; 
        public int spawnCount; // number of enemies all ready spawned in currrent wave

    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }


    public List<Wave> waves;
    
    public int currentWaveCount;

    [Header("Spawner Attributes")]
    float Spawntimer;
    public int enemiesAlive;
    public int MaxEnemies;
    public bool MaxEnemiesReached;
    public float waveInterval;

    [Header("Spawnpositions")]
    public List<Transform> Spawnpositions;



    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) {
            StartCoroutine(BeginWave());
        }

        Spawntimer += Time.deltaTime;

        if (Spawntimer >= waves[currentWaveCount].spawnInterval) 
        {
            Spawntimer = 0f;
            SpawnEnemies() ;
        }
    }

    IEnumerator BeginWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveCount < waves.Count -1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups) 
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !MaxEnemiesReached )
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= MaxEnemies)
                    {
                        MaxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + Spawnpositions[Random.Range(0, Spawnpositions.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        if(enemiesAlive < MaxEnemies)
        {
            MaxEnemiesReached = false;
        }
    }
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
