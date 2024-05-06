using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _target;
    private float _speed = 10.0f;
    
    void Start()
    {
        _target = GameManager.Instance.trackedEnemies[0];
    }

    private void Update()
    {
        
        Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;
        transform.Translate(directionToTarget * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _target)
            Destroy(gameObject);
    }
}
