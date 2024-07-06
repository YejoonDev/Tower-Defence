using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackTower : Tower 
{
    // Serialized variables
    [SerializeField] private float speed = 10;
    [SerializeField] private int damage = 10;
    // Inspector variables
    [SerializeField] private Transform launchModel;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    // private variables
    private Transform _target;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        
        // 회전
        if (_target != null)
        {
            Quaternion lookRotation = Quaternion.LookRotation(_target.position - launchModel.position);
            launchModel.rotation = Quaternion.Slerp(launchModel.rotation, lookRotation, 5.0f * Time.deltaTime);
            launchModel.rotation = Quaternion.Euler(0, launchModel.rotation.eulerAngles.y, 0);
        }
        
        // 가장 가까운 적 타겟으로 지정 
        if (EnemiesUpdated)
        {
            if (enemiesInRange.Count > 0)
                SetClosestTarget();
            else
                _target = null;
        }
    }
    
    private void SetClosestTarget()
    {
        float minDistance = this.range + 1;
        foreach (EnemyController enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    _target = enemy.transform;
                }
            }
        }
    }

    protected override bool CanAttack()
    {
        if (FireTimer >= fireInterval && _target != null)
            return true;
        
        return false;
    }
    protected override void Attack()
    {
        FireTimer = 0;
        firePoint.LookAt(_target);
        GameObject projectileObject =
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.InitializeProjectile(_target.gameObject, speed, damage);
    }
}
