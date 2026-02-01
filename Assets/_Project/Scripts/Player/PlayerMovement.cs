using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();

        // Animatorに移動方向を渡す（将来の上下アニメ用にYも渡す）
        _animator.SetFloat("MoveX", _moveInput.x);
        _animator.SetFloat("MoveY", _moveInput.y);

        // 移動しているかどうか
        bool isMoving = (_moveInput.sqrMagnitude > 0.01f);
        _animator.SetBool("isMoving", isMoving);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.linearVelocity = _moveInput * _moveSpeed;
    }
}
