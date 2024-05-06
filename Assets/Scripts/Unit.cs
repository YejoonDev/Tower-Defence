using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject projectilePrefab;
    private Transform _muzzleTransform;
    void Start()
    {
        _muzzleTransform = transform.Find("Muzzle");
        StartCoroutine(Attack());
    }
    
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (GameManager.Instance.trackedEnemies.Count != 0)
            {
                Instantiate(projectilePrefab, _muzzleTransform.position, projectilePrefab.transform.rotation);
            }
        }
        
    }
}
