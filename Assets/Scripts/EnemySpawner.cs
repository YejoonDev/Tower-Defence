using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    public int enemySpawnNumber = 10;
    public GameObject[] enemyWaveArray;
    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentMode == ModeState.Normal && Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.currentMode = ModeState.Wave;
            UIManager.Instance.modeTitle.text = "Wave";
            UIManager.Instance.roundText.text = "Round : " + (GameManager.Instance.currentRound + 1);
            StartCoroutine(SpawnRoutine(GameManager.Instance.currentRound));
            GameManager.Instance.currentRound++;
        }
    }
    
    public IEnumerator SpawnRoutine(int idx)
    {
        for (int i = 0; i < enemySpawnNumber; i++)
        {
            GameObject obj = Instantiate(enemyWaveArray[idx].gameObject, transform.position, 
                enemyWaveArray[idx].gameObject.transform.rotation);
            GameManager.Instance.trackedEnemies.Add(obj);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
