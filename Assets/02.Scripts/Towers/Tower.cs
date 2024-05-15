using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float range = 5.0f;

    public LayerMask whatIsEnemy;
    
    public GameObject projectilePrefab;
    private Transform _firePoint;

    public Transform launchModel;
    
    public List<Enemy> enemiesInRange = new List<Enemy>();
    private Collider[] colliderInRange;
    
    private GameObject detectionZoneVisualization;
    
    [SerializeField] private float detectionZoneRadius;

    [SerializeField] private float detectionInterval = 0.2f;
    private float _detectionTimer = 0;

    [SerializeField] private float fireInterval = 2.0f;
    private float _fireTimer = 0;

    private Transform _target;
    public bool enemiesUpdated;
    void Start()
    {
        _firePoint = transform.Find("Fire Point");
        
        detectionZoneVisualization = transform.Find("Detection Zone Visualization").gameObject;
        detectionZoneVisualization.transform.localScale = new Vector3(detectionZoneRadius*2, 0, detectionZoneRadius*2);
        // StartCoroutine(Attack());
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
        if (_detectionTimer >= detectionInterval)
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

    private void DetectEnemiesInRange()
    {
        colliderInRange = Physics.OverlapSphere(transform.position, range, whatIsEnemy);
        enemiesInRange.Clear();
        foreach (Collider col in colliderInRange)
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
        projectile.Initialize(_target.gameObject, speed, damage);
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
    
    public void DetectionZoneOnOff()
    {
        if (detectionZoneVisualization.activeInHierarchy)
        {
            detectionZoneVisualization.SetActive(false);
        }
        else
        {
            detectionZoneVisualization.SetActive(true);
        }
            
    }
    
    
}
