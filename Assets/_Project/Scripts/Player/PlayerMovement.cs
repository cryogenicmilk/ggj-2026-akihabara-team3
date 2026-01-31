using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float _moveSpeed = 5f;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * _moveSpeed;
    }
}
