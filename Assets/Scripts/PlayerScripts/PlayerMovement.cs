using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    private PlayerInputActions playerInputActions;
    private Animator animator;
    private Transform playerMesh;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerMesh = GetComponentInChildren<Transform>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Update()
    {
        if (rb.velocity.magnitude > 0.01) leanForward();
        else playerMesh.rotation = Quaternion.identity;
        animator.SetBool("isMoving", rb.velocity.magnitude > 0.01);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 velocity = new Vector3(input.x, 0, input.y).normalized * speed;
        Vector3 pos = rb.position;
        pos.y = 1.5f;

        rb.velocity = velocity;
        rb.MovePosition(pos);
    }

    private void leanForward()
    {
        Vector3 y_axis = new Vector3(0, 1, 0);
        playerMesh.rotation = Quaternion.FromToRotation(y_axis, rb.velocity + 30*y_axis);
    }
}
