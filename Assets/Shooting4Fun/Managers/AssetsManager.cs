using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance;
    public Player player;
    public GameObject bulletHolder;

    public Player Player { get { return player; } }
    public GameObject BulletHolder { get { return bulletHolder; } }

    public GameObject[] EnemySpawners { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetEnemySpawners();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void SetEnemySpawners()
    {
        EnemySpawners = GameObject.FindGameObjectsWithTag(Tags.ENEMY_SPAWNER);
    }
}
