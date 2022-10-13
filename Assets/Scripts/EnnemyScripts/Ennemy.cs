using UnityEngine;

public class Ennemy : AbstractEnnemy
{
    public override void moveTowardsTarget()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 position = transform.position;
        Vector3 velocity = (targetPosition - position).normalized * movementSpeed;
        rb.velocity = velocity;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
