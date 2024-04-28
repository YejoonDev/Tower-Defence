using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour
{
    [FormerlySerializedAs("isExisted")] [FormerlySerializedAs("ixExisted")] public bool _isExisted;
    public GameObject towerUnit;
    private Transform _spawnPos;
    [SerializeField] private Material _material;
    private CapsuleCollider _unitCollider;
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.SetColor("_Color",new Color(0.5f,0.5f,0.5f));
        _spawnPos = transform.Find("Spawn Pos");
        _unitCollider = towerUnit.GetComponent<CapsuleCollider>();
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
        Vector3 spawnPos = new Vector3(_spawnPos.position.x, _spawnPos.position.y + _unitCollider.height / 2,
            _spawnPos.position.z);
        
        _isExisted = true;
    }
}
