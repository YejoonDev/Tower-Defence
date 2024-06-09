using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // readOnly variables
    private readonly float _detectionInterval = 0.2f;
    // serialize variables
    [SerializeField] public int cost;
    
    [SerializeField] private float range = 5.0f;
    [SerializeField] private float fireInterval = 2.0f;
    // public variables
    public List<EnemyController> enemiesInRange = new List<EnemyController>();
    
    public Transform launchModel;
    public LayerMask whatIsEnemy;
    public bool enemiesUpdated;
    // private variables
    private GameObject _rangeIndicator;
    private IAttack _attackBehavior;
    private ITargetAttack _targetAttackBehavior;
    private ProjectileAttack _projectileAttack;
    private SlowdownAttack _slowdownAttack;
    private Transform _target;
    private Collider[] _colliderInRange;
    private float _detectionTimer = 0;
    private float _fireTimer = 0;

    private void Awake()
    {
        _rangeIndicator = transform.Find("Range Indicator").gameObject;
    }

    private void Start()
    {
        _attackBehavior = GetComponent<IAttack>();
        _targetAttackBehavior = _attackBehavior as ITargetAttack;
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
        
        // 공격 
        _fireTimer += Time.deltaTime;
        if (_fireTimer >= fireInterval && _target != null)
        {
            _fireTimer = 0;
            if (_targetAttackBehavior != null)
            {
                _targetAttackBehavior.Attack(_target);
            }
            else
            {
                _attackBehavior.Attack();
            }
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
        _colliderInRange = Physics.OverlapSphere(transform.position, range/ 2, whatIsEnemy);
        enemiesInRange.Clear();
        foreach (Collider col in _colliderInRange)
        {
            enemiesInRange.Add(col.GetComponent<EnemyController>());
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
    
    private void OnDrawGizmos()
    {
        // 스피어의 색상을 설정합니다.
        Gizmos.color = Color.red;
        
        // 스피어를 그립니다.
        Gizmos.DrawWireSphere(transform.position, range/ 2);
    }
}