using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    private Path path;
    private int currentPoint;
    private bool reachedEnd;
    public List<GameObject> assailantList;

    private void Awake()
    {
        path = FindObjectOfType<Path>();
        currentPoint = 0;
        assailantList = new List<GameObject>();
    }
    

    void Update()
    {
        if (!reachedEnd)
        {
            float distance = (path.points[currentPoint].transform.position - transform.position).magnitude;
            if (distance < 0.2f)
            {
                transform.position = path.points[currentPoint].transform.position;
                currentPoint++;
                if (currentPoint >= path.points.Length)
                {
                    reachedEnd = true;
                }
                else
                {
                    transform.LookAt(path.points[currentPoint]);
                }
            }
        }
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    public void DestroyEnemy()
    {
        // 자신을 공격중인 유닛의 범위 리스트에서 삭제
        foreach (GameObject obj in assailantList)
        {
            Tower tower = obj.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                tower.enemiesInRange.Remove(this.gameObject.GetComponent<Enemy>());
            }
        }
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damageAmount)
    {
        hp -= damageAmount;
        if (hp <= 0)
        {
            hp = 0;
            DestroyEnemy();
        }
    }
}
