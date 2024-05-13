using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거");
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.trackedEnemies.Remove(other.gameObject);
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DestroyEnemy();
            }
        }
    }
}
