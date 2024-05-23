using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // readOnly variables
    private readonly float _detectionInterval = 0.2f;
    // inspector variables
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float range = 5.0f;
    [SerializeField] private float fireInterval = 2.0f;
    // public variables
    public List<Enemy> enemiesInRange = new List<Enemy>();
    public GameObject projectilePrefab;
    public Transform launchModel;
    public LayerMask whatIsEnemy;
    public bool enemiesUpdated;
    // private variables
    private GameObject _rangeIndicator;
    private Transform _firePoint;
    private Transform _target;
    private Collider[] _colliderInRange;
    private float _detectionTimer = 0;
    private float _fireTimer = 0;

    private void Awake()
    {
        _firePoint = transform.Find("Fire Point");
        _rangeIndicator = transform.Find("Range Indicator").gameObject;
    }
    
    private void Update()
    {
        enemiesUpdated = false;
        
        // 회전
        if (_target != null)
        {
            Quaternion lookRotation = Quaternion.LookRotation(_target.position - launchModel.position);
            launchModel.rotation = Quaternion.Slerp(launchModel.rotation, lookRotation, 5.0f * Time.deltaTime);
            launchModel.rotation = Quaternion.Euler(0, launchModel.rotation.eulerAngles.y, 0);
        }
        
        // 감지
        _detectionTimer += Time.deltaTime;
        if (_detectionTimer >= _detectionInterval)
        {
            _detectionTimer = 0;
            DetectEnemiesInRange();
            enemiesUpdated = true;
        }
        
        // 발사 
        _fireTimer += Time.deltaTime;
        if (_fireTimer >= fireInterval && _target != null)
        {
            _fireTimer = 0;
            FireProjectile();
        }

        // 가장 가까운 적 타겟으로 지정 
        if (enemiesUpdated)
        {
            if (enemiesInRange.Count > 0)
                SetClosestTarget();
            else
                _target = null;
        }
    }

    public void InitializeIndicatorTower()
    {
        enabled = false;
        GetComponent<Collider>().enabled = false;
        _rangeIndicator.SetActive(true);
        _rangeIndicator.transform.localScale = new Vector3(range, 0.2f, range);
    }

    private void DetectEnemiesInRange()
    {
        _colliderInRange = Physics.OverlapSphere(transform.position, range, whatIsEnemy);
        enemiesInRange.Clear();
        foreach (Collider col in _colliderInRange)
        {
            enemiesInRange.Add(col.GetComponent<Enemy>());
        }
    }
    private void FireProjectile()
    {
        _firePoint.LookAt(_target);
        GameObject projectileObject =
            Instantiate(projectilePrefab, _firePoint.position, _firePoint.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.InitializeProjectile(_target.gameObject, speed, damage);
    }
    private void SetClosestTarget()
    {
        float minDistance = this.range + 1;
        foreach (Enemy enemy in enemiesInRange)
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
}