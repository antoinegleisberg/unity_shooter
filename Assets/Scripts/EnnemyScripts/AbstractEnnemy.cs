using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnnemy : MonoBehaviour
{
    public float movementSpeed;
    public float attackSpeed;
    public int maxHealth;
    public int attackDamage;
    public float timeBeforeAttack;
    public float attackRange;

    private float health;

    public Player target;
    public Rigidbody rb;

    private Transform ennemyMesh;

    EnnemyBaseState currentState;
    public EnnemyAttackState attackState = new EnnemyAttackState();
    public EnnemyMoveState moveState = new EnnemyMoveState();
    public EnnemyIdleState idleState = new EnnemyIdleState();

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player")?.GetComponent<Player>();
        ennemyMesh = GetComponentInChildren<Transform>();
        currentState = moveState;
        timeBeforeAttack = 0;
    }

    public void switchState(EnnemyBaseState newState)
    {
        currentState.LeaveState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        if (target == null) switchState(idleState);
        timeBeforeAttack -= Time.deltaTime;
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        if (target == null) switchState(idleState);
        currentState.FixedUpdateState(this);
    }

    public abstract void moveTowardsTarget();

    public void attackTarget(Player target)
    {
        if (timeBeforeAttack <= 0)
        {
            target.takeDamage(this.attackDamage);
            StartCoroutine(playAttackAnimation(target));
            timeBeforeAttack = 1 / attackSpeed;
        }
        if (!target.isAlive()) switchState(idleState);
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) killEntity();
    }

    public void killEntity()
    {
        Destroy(this.gameObject);
    }

    IEnumerator playAttackAnimation(Player target)
    {
        Vector3 y_axis = new Vector3(0, 1, 0);
        Vector3 targetRotation = (target.transform.position - transform.position) + 3 * y_axis;
        ennemyMesh.rotation = Quaternion.FromToRotation(y_axis, Vector3.Lerp(y_axis, targetRotation, 0.5f));
        yield return new WaitForSeconds(0.1f);
        ennemyMesh.rotation = Quaternion.FromToRotation(y_axis, Vector3.Lerp(y_axis, targetRotation, 1));
        yield return new WaitForSeconds(0.1f);
        ennemyMesh.rotation = Quaternion.FromToRotation(y_axis, Vector3.Lerp(y_axis, targetRotation, 0.5f));
        yield return new WaitForSeconds(0.1f);
        ennemyMesh.rotation = Quaternion.identity;
    }
}
