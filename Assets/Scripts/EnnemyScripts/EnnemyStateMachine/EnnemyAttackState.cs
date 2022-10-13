using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAttackState : EnnemyBaseState
{
    public override void EnterState(AbstractEnnemy ennemy)
    {
        ennemy.attackTarget(ennemy.target);
    }

    public override void UpdateState(AbstractEnnemy ennemy)
    {
        if (Vector3.Distance(ennemy.transform.position, ennemy.target.transform.position) > ennemy.attackRange + 0.5)
        {
            ennemy.switchState(ennemy.moveState);
        }
        else
        {
            ennemy.attackTarget(ennemy.target);
        }
    }
    public override void FixedUpdateState(AbstractEnnemy ennemy) { }

    public override void LeaveState(AbstractEnnemy ennemy)
    {

    }
}
