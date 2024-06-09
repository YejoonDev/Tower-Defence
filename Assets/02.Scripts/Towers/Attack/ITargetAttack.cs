using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetAttack : IAttack
{
    void Attack(Transform target);
}
