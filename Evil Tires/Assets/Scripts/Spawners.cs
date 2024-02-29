using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public float spawningRate = 2f;
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    private float LastSpawnTime;

    void Update()
    {
        if (LastSpawnTime + spawningRate < Time.time)
        {
            var randomSpawnPoints = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
            Instantiate(zombiePrefab, randomSpawnPoints.position, Quaternion.identity);
            LastSpawnTime = Time.time;
        }
    }
}
