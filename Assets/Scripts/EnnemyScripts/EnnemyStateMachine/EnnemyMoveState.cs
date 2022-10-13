using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMoveState : EnnemyBaseState
{
    public override void EnterState(AbstractEnnemy ennemy) { }

    public override void UpdateState(AbstractEnnemy ennemy)
    {
        if (ennemy.target == null)
        {
            ennemy.switchState(ennemy.idleState);
            return;
        }
        if (Vector3.Distance(ennemy.transform.position, ennemy.target.transform.position) < ennemy.attackRange)
        {
            ennemy.switchState(ennemy.attackState);
        }
    }
    public override void FixedUpdateState(AbstractEnnemy ennemy)
    {
        ennemy.moveTowardsTarget();
    }

    public override void LeaveState(AbstractEnnemy ennemy) { }
}