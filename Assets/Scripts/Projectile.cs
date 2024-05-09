using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 10.0f;
    

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.Translate( Time.deltaTime * speed * dir);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
            Destroy(gameObject);
    }
}
