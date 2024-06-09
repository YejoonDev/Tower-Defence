using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownAttack : MonoBehaviour, IAttack
{
    private Tower _theTower;
    [SerializeField] private float duration;
    [SerializeField] private float speedModifier;

    private void Start()
    {
        _theTower = GetComponent<Tower>();
    }

    public void Attack()
    {
        foreach (EnemyController enemy in _theTower.enemiesInRange)
        {
            StartCoroutine(ApplyDeBuff(enemy));
        }
    }

    private IEnumerator ApplyDeBuff(EnemyController enemy)
    {
        enemy.speedModifier = speedModifier;
        yield return new WaitForSeconds(duration);
        enemy.RemoveDeBuff();
    }
}
