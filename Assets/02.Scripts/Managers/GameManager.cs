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
    public List<EnemyController> ActiveEnemies = new List<EnemyController>();
    public int currentRound;
    public LayerMask unitLayerMask;
    public bool levelActive;
    private EnemySpawner enemySpawner;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        currentRound = 0;
    }

    private void Update()
    {
        if (ActiveEnemies.Count == 0 && enemySpawner.remainingEnemiesToSpawn == 0)
        {
            levelActive = false;
        }
    }

    
    
    
    
}
