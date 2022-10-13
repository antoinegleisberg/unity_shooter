using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnnemyBaseState
{
    public abstract void EnterState(AbstractEnnemy ennemy);
    public abstract void UpdateState(AbstractEnnemy ennemy);
    public abstract void FixedUpdateState(AbstractEnnemy ennemy);
    public abstract void LeaveState(AbstractEnnemy ennemy);
}
