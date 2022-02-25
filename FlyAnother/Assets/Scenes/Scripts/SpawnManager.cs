using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform playerPosition;

    [SerializeField] private float spanwRangeX = 20;
    [SerializeField] private float spawnRangeY = 20;
    [SerializeField] private float spawnDistance = 50;    
    [SerializeField] private float startDelay = 2;
    [SerializeField] private float spawnInterval = 3;
    
    void Start()
    {
        InvokeRepeating("SpawnRandomObstacle", startDelay, spawnInterval);
    }
      
    private void SpawnRandomObstacle()
    {
        Vector3 spawnPos = new Vector3(Random.Range(transform .position.x-spanwRangeX, transform.position.x + spanwRangeX), Random.Range(transform.position.y - spawnRangeY, transform.position.y + spawnRangeY), transform.position.z);
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
        }
    }    
}
