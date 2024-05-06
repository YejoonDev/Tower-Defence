using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> towersToBuild;
    public List<GameObject> enemies;
    public Vector3 enemySpawnPos;
    public List<GameObject> trackedEnemies = new List<GameObject>();
    public int enemyLimitCount = 10;
    public int currentEnemyLevel;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentEnemyLevel = 0;
    }

    private void Update()
    {
        if (trackedEnemies.Count == 0 && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(SpawnRoutine(currentEnemyLevel));
            currentEnemyLevel++;
        }
    }

    public void SpawnRandomTower(Vector3 spawnPos)
    {
        int randIdx = Random.Range(0, towersToBuild.Count);
        CapsuleCollider unitCollider = towersToBuild[randIdx].GetComponent<CapsuleCollider>();
        Instantiate(towersToBuild[randIdx], spawnPos, towersToBuild[randIdx].transform.rotation);
    }
    
    IEnumerator SpawnRoutine(int idx)
    {
        for (int i = 0; i < enemyLimitCount; i++)
        {
            GameObject obj = Instantiate(enemies[idx].gameObject, enemySpawnPos, 
                enemies[idx].gameObject.transform.rotation);
            trackedEnemies.Add(obj);
            yield return new WaitForSeconds(2.0f);
        }
        
    }
    
    
}
