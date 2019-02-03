using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Instance;
    public int EnemyKills { get; private set; } = 0;

    public GameObject[] enemyPrefabs;
    public int maxEnemies = 20;
    public float spawnEnemiesTimeout = 60;

    private int currentEnemies = 0;


    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnEnemies();
    }

    public void EnemyKilled()
    {
        EnemyKills ++;
        currentEnemies --;
    }

    private void SpawnEnemies()
    {
        while (currentEnemies < maxEnemies){
            SpawnEnemy();
            currentEnemies ++;
        }
        Invoke("SpawnEnemies", spawnEnemiesTimeout);
    }

    private void SpawnEnemy()
    {
        var spawners = AssetsManager.Instance.EnemySpawners;
        var spawner = spawners[Random.Range(0, spawners.Length)];
        var enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
        enemy.transform.position = spawner.transform.position;
        enemy.transform.SetParent(spawner.transform);
    }
}
