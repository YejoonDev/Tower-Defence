using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Unit unit = GetComponentInParent<Unit>();
            if (unit != null)
            {
                unit.enemiesInRange.Add(other.gameObject);
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
            Unit unit = GetComponentInParent<Unit>();
            if (unit != null)
            {
                unit.enemiesInRange.Remove(other.gameObject);
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.assailantList.Remove(this.gameObject);
                }
            }
        }
    }
}
