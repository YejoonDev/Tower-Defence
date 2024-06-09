using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour, ITargetAttack
{
    // serialized variables
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    // public variables
    public GameObject projectilePrefab;
    private Transform _firePoint;

    private void Awake()
    {
        _firePoint = transform.Find("Fire Point");
    }
    public void Attack(Transform target)
    {
        _firePoint.LookAt(target);
        GameObject projectileObject =
            Instantiate(projectilePrefab, _firePoint.position, _firePoint.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.InitializeProjectile(target.gameObject, speed, damage);
    }

    public void Attack() { }
}
