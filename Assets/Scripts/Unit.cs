using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform MuzzleTransform;
    void Start()
    {
        StartCoroutine(Attack());
    }
    void Update()
    {
        
    }

    
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (GameManager.Instance.trackedEnemies.Count != 0)
            {
                Instantiate(projectilePrefab, MuzzleTransform.position, projectilePrefab.transform.rotation);
            }
        }
        
    }
}
