using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Tower tower = GetComponentInParent<Tower>();
            if (tower != null)
            {
                tower.enemiesInRange.Add(other.gameObject);
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.assailantList.Add(transform.parent.gameObject);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Tower tower = GetComponentInParent<Tower>();
            if (tower != null)
            {
                tower.enemiesInRange.Remove(other.gameObject);
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.assailantList.Remove(this.gameObject);
                }
            }
        }
    }
}
