using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject projectilePrefab;
    private Transform _muzzleTransform;
    public List<GameObject> enemiesInRange;
    
    private SphereCollider detectionZone;
    private GameObject detectionZoneVisualization;

    [SerializeField] private float detectionZoneRadius; 

    private void Awake()
    {
        enemiesInRange = new List<GameObject>();
    }

    void Start()
    {
        _muzzleTransform = transform.Find("Muzzle");
        detectionZone = transform.Find("Detection Zone").GetComponent<SphereCollider>();
        detectionZone.radius = detectionZoneRadius;
        detectionZoneVisualization = transform.Find("Detection Zone Visualization").gameObject;
        detectionZoneVisualization.transform.localScale = new Vector3(detectionZoneRadius*2, 0, detectionZoneRadius*2);
        StartCoroutine(Attack());
    }

    private void Update()
    {
        if (enemiesInRange.Count != 0)
        {
            Vector3 targetDirection = enemiesInRange[0].transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }
        
        
    }
    IEnumerator Attack()
    {
        while (true)
        {
            yield return null;
            if (enemiesInRange.Count != 0)
            {
                GameObject projectileObject
                    = Instantiate(projectilePrefab, _muzzleTransform.position, projectilePrefab.transform.rotation);
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.target = enemiesInRange[0];
                yield return new WaitForSeconds(2.0f);
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
