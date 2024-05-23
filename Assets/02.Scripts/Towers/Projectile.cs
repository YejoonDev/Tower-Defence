using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _target;
    private BoxCollider _targetBoxCollider;
    private float _speed;
    private int _damage;
    private ParticleSystem _particleSystem;
    private void Update()
    {
        
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetDirection = ((_target.transform.position + _targetBoxCollider.center) - transform.position).normalized;
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
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(_damage);
            }
            Destroy(gameObject);
        }
            
    }
}
