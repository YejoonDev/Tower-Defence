using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // serialize variable
    [SerializeField] private float speed;
    [SerializeField] private int rewardMoney;
    // public variables
    public List<GameObject> assailantList = new List<GameObject>();
    // private variables
    private Path _path;
    private int _currentPoint;
    private bool _hasReachedEnd;
    
    private void Start()
    {
        _path = FindObjectOfType<Path>();
        GameManager.Instance.ActiveEnemies.Add(this);
    }

    void Update()
    {
        if (!_hasReachedEnd)
        {
            float distance = (_path.points[_currentPoint].transform.position - transform.position).magnitude;
            if (distance < 0.2f)
            {
                transform.position = _path.points[_currentPoint].transform.position;
                _currentPoint++;
                if (_currentPoint >= _path.points.Length)
                {
                    _hasReachedEnd = true;
                }
                else
                {
                    transform.LookAt(_path.points[_currentPoint]);
                }
            }
        }
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    public void DestroyEnemy()
    {
        MoneyManager.Instance.GiveMoney(rewardMoney);
        foreach (GameObject obj in assailantList)
        {
            Tower tower = obj.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                tower.enemiesInRange.Remove(this.gameObject.GetComponent<EnemyController>());
            }
        }
        GameManager.Instance.ActiveEnemies.Remove(this);
        Destroy(this.gameObject);
    }

    
}
