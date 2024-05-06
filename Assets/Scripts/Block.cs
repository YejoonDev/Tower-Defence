using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour
{
    public bool _isExisted;
    private Transform _spawnPos;
    void Start()
    {
        _spawnPos = transform.Find("Spawn Pos");
    }

    private void OnMouseDown()
    {
        if (!_isExisted)
        {
            SpawnTowerUnit();
        }
    }

    void SpawnTowerUnit()
    {
        GameManager.Instance.SpawnRandomTower(_spawnPos.position);
        _isExisted = true;
    }
}
