using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementStates
    {
        Stay,
        Run,
        InAir,
        InAirRun
    }

    [Header("State")]
    public MovementStates movementState;

    [Header("Player Components")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Collider playerCollider;

    [Header("Player Movenet Settings")]
    [SerializeField, Range(0f, 10f)] public float speed;
    [SerializeField, Range(0f, 20f)] private float jumpForce;


    private float _sprintForce = 1f;
    private Vector2 _inputVector;

    private void Update()
    {
        Run();
    }

    public void SetSprintSpeed(float value) => _sprintForce = value;
    public void OnMove(InputValue input)
    {
        _inputVector = input.Get<Vector2>();
    }

    public void OnJump()
    {
        if (!IsGrounded()) return;
        playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
        movementState = MovementStates.InAirRun;

        print("jump");
    }

    public bool IsGrounded()
    {
        float _distanceToTheGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(playerRb.position, Vector3.down, _distanceToTheGround + 0.1f);
    }

    private void Run()
    {
        Vector3 direction = (_inputVector.x * playerRb.transform.right + _inputVector.y * playerRb.transform.forward) * speed * _sprintForce;
        playerRb.velocity = new Vector3(direction.x, playerRb.velocity.y, direction.z);
    }
}
