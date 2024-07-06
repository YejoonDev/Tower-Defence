using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffTower : Tower
{
    //  Serialize Field
    [SerializeField] private float duration;
    [SerializeField] private float speedModifier;

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override bool CanAttack()
    {
        if (FireTimer >= fireInterval && enemiesInRange.Count != 0)
            return true;
        
        return false;
    }

    protected override void Attack()
    {
        FireTimer = 0;
        foreach (EnemyController enemy in enemiesInRange)
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
