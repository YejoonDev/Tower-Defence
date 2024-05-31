using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    // serialized variables
    [SerializeField] private int enemySpawnNumber = 10;
    // public variables
    public int remainingEnemiesToSpawn;
    public GameObject[] enemyWaveArray;


    private void Awake()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!GameManager.Instance.levelActive)
            {
                remainingEnemiesToSpawn = enemySpawnNumber;
                GameManager.Instance.levelActive = true;
                UIManager.Instance.roundText.text = "Round : " + (GameManager.Instance.currentRound + 1);
                StartCoroutine(SpawnRoutine(GameManager.Instance.currentRound));
                GameManager.Instance.currentRound++;
            }
        }
    }
    
    public IEnumerator SpawnRoutine(int idx)
    {
        for (int i = 0; i < enemySpawnNumber; i++)
        {
            GameObject obj = Instantiate(enemyWaveArray[idx].gameObject, transform.position, 
                enemyWaveArray[idx].gameObject.transform.rotation);
            remainingEnemiesToSpawn--;
            yield return new WaitForSeconds(2.0f);
        }
    }
}
