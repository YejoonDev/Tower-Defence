using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> towersToBuild;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRandomTower(Vector3 spawnPos)
    {
        int randIdx = Random.Range(0, towersToBuild.Count);
        Instantiate(towersToBuild[randIdx], spawnPos, towersToBuild[randIdx].transform.rotation);
    }
}
