using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (GameManager.Instance.trackedEnemies.Contains(other.gameObject))
            {
                
                Destroy(other.gameObject);
                GameManager.Instance.trackedEnemies.Remove(other.gameObject);
            }
            else
            {
                Debug.Log("존재하지 않습니다");
            }
            ;
            
        }
        
    }
}
