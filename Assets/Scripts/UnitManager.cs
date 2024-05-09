using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public GameObject[] deployableUnits;
    
    
    
    public LayerMask blockLayer;
    
    
    
    

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
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.currentMode == ModeState.Build)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayer))
            {
                Block block = hit.collider.GetComponent<Block>();
                Transform spawnPos = hit.collider.transform.Find("Spawn Pos");
                if (!block.isExisted)
                {
                    SpawnRandomUnit(spawnPos.position);
                    block.isExisted = true;
                }
            }
        }
    }
    
    public void SpawnRandomUnit(Vector3 spawnPos)
    {
        int randIdx = Random.Range(0, deployableUnits.Length);
        Instantiate(deployableUnits[randIdx], spawnPos, deployableUnits[randIdx].transform.rotation);
    }
    
    
}
