using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum TowerType
{
    Fire,
    Ice,
    Electronic,
    Magic,
    Normal,
}
public class Tower : MonoBehaviour
{
    // static variables
    
    // readOnly variables
    private readonly float _detectionInterval = 0.2f;
    // serialize variables
    [SerializeField] private TowerType towerType;
    [SerializeField] private GameObject rangeCircle;
    [SerializeField] public int cost = 100;
    [SerializeField] protected float range = 5.0f;
    [SerializeField] protected float fireInterval = 2.0f;
    [SerializeField] private float rangeCircleScaleFactor = 1;
    [SerializeField] private int level = 1;
    // public variables
    public List<EnemyController> enemiesInRange = new List<EnemyController>();
    // protected variables
    protected bool EnemiesUpdated;
    protected float FireTimer = 0;
    // private variables
    private Collider[] _colliderInRange;
    private LayerMask _whatIsEnemy;
    private float _detectionTimer = 0;

    protected virtual void Awake()
    {
        _whatIsEnemy = LayerMask.GetMask("Enemy");
    }

    protected virtual void Start()
    {
        rangeCircle.transform.localScale = 
            new Vector3(range * rangeCircleScaleFactor, 0.2f, range * rangeCircleScaleFactor);
        UpdateRangeCircleColor();
        DeActiveRangeCircle();
    }

    protected virtual void Update()
    {
        EnemiesUpdated = false;
        
        _detectionTimer += Time.deltaTime;
        FireTimer += Time.deltaTime;
        
        // 감지
        if (_detectionTimer >= _detectionInterval)
        {
            _detectionTimer = 0;
            DetectEnemiesInRange();
            EnemiesUpdated = true;
        }
        
        // 공격 
        if (CanAttack())
        {
            Attack();
        }
    }

    protected virtual bool CanAttack() { return false; }
    protected virtual void Attack() { }

    public void SetupIndicatorTower()
    {
        enabled = false;
        GetComponent<Collider>().enabled = false;
        UpdateRangeCircleColor();
        ActiveRangeCircle();
    }

    private void DetectEnemiesInRange()
    {
        _colliderInRange = Physics.OverlapSphere(transform.position, range/ 2, _whatIsEnemy);
        enemiesInRange.Clear();
        foreach (Collider col in _colliderInRange)
        {
            enemiesInRange.Add(col.GetComponent<EnemyController>());
        }
    }

    public void Upgrade()
    {
        Tower[] upgradePrefabs = null;
        switch (towerType)
        {
            case TowerType.Normal:
                upgradePrefabs = TowerManager.Instance.normalTowerVersions;
                break;
            case TowerType.Fire:
                upgradePrefabs = TowerManager.Instance.fireTowerVersions;
                break;
            case TowerType.Electronic:
                upgradePrefabs = TowerManager.Instance.electronicTowerVersions;
                break;
            case TowerType.Ice:
                upgradePrefabs = TowerManager.Instance.iceTowerVersions;
                break;
            case TowerType.Magic:
                upgradePrefabs = TowerManager.Instance.magicTowerVersions;
                break;
        }
        
        if (upgradePrefabs != null && level < upgradePrefabs.Length)
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Destroy(gameObject);
            Instantiate(upgradePrefabs[level], position, rotation);
            UIManager.Instance.CloseTowerDetailsPanel();
        }
        else
        {
            Debug.Log("업그레이드 불가");
        }
    }
    public void ActiveRangeCircle() { rangeCircle.SetActive(true); }
    public void DeActiveRangeCircle() { rangeCircle.SetActive(false); }
    public void UpdateRangeCircleColor()
    {
        MeshRenderer meshRenderer = rangeCircle.GetComponentInChildren<MeshRenderer>();
        Color color = Color.white;
        switch (towerType)
        {
            case TowerType.Electronic:
                color = Color.yellow;
                break;
            case TowerType.Fire:
                color = Color.red;
                break;
            case TowerType.Ice:
                color = Color.cyan;
                break;
            case TowerType.Normal:
                color = Color.gray;
                break;
            case TowerType.Magic:
                color = Color.magenta;
                break;
        }
        meshRenderer.material.color = color;
    }
    private void OnDrawGizmos()
    {
        // 스피어의 색상을 설정합니다.
        Gizmos.color = Color.red;
        
        // 스피어를 그립니다.
        Gizmos.DrawWireSphere(transform.position, range/ 2);
    }
}