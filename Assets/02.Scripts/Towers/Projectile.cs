using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // private variables
    private GameObject _target;
    private BoxCollider _targetBoxCollider;
    private float _speed;
    private int _damage;

    private void Update()
    {
        
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 targetCenter = _target.transform.TransformPoint(_targetBoxCollider.center);
        Vector3 targetDirection = (targetCenter - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.Translate( Time.deltaTime * _speed * Vector3.forward);
        transform.rotation = targetRotation;
    }

    public void InitializeProjectile(GameObject target, float speed, int damage)
    {
        _target = target;
        _targetBoxCollider = _target.GetComponent<BoxCollider>();
        _speed = speed;
        _damage = damage;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _target.gameObject)
        {
            EnemyHealthController enemyHealthController = other.GetComponent<EnemyHealthController>();
            enemyHealthController.TakeDamage(_damage);
            Destroy(gameObject);
        }
            
    }
}
