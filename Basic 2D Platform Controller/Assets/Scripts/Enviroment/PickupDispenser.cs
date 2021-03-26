using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDispenser : MonoBehaviour
{
    [SerializeField]
    private BulletPickup bulletPickup;

    [SerializeField]
    private int capacityOfSpawner = 1;

    [SerializeField]
    private float spawnWaitTime = 5f;

    private bool needToSpawn;

    private float lastSpawnTime,
        lastShortTime;

    public Animator Anim { get; private set; }

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CheckSpawnerCapacity())
        {
            needToSpawn = false;
        }
        else if (!CheckSpawnerCapacity() && !needToSpawn)
        {
            lastShortTime = Time.time;
            needToSpawn = true;
        }
        else if (!CheckSpawnerCapacity())
        {
            needToSpawn = true;
        }

        if (needToSpawn)
        {
            Anim.SetBool("isSpawning", true);
            if (Time.time >= lastSpawnTime + spawnWaitTime)
            {
                if (Time.time >= lastShortTime + spawnWaitTime)
                {
                    SpawnEnemy();
                    lastSpawnTime = Time.time;
                    if (CheckSpawnerCapacity())
                    {
                        Anim.SetBool("isSpawning", false);
                    }
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(bulletPickup, transform, false);
    }

    private bool CheckSpawnerCapacity()
    {
        BulletPickup[] spawnedEnemies = GetComponentsInChildren<BulletPickup>();
        return spawnedEnemies.Length >= capacityOfSpawner;
    }
}
