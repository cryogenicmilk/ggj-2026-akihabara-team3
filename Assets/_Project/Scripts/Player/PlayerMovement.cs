using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] SpriteRenderer spriteRenderer;

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
    }

    private void FixedUpdate()
    {
        Move();
        UpdateAnimation();
    }

    private void Move()
    {
        _rb.linearVelocity = _moveInput * _moveSpeed;
    }

    void UpdateAnimation()
    {
        bool isWalking = _moveInput.magnitude > 0.1f;
        _animator.SetBool("Walk", isWalking);

        // ‘S•”false‚É‚·‚é
        _animator.SetBool("FaceUp", false);
        _animator.SetBool("FaceDown", false);
        _animator.SetBool("FaceLeft", false);
        _animator.SetBool("FaceRight", false);

        float threshold = 0.3f; // “ü—Í”»’è‚Ì‚µ‚«‚¢’l

        if (_moveInput.y > threshold)
        {
            _animator.SetBool("FaceUp", true);
        }
        else if (_moveInput.y < -threshold)
        {
            _animator.SetBool("FaceDown", true);
        }
        else if (_moveInput.x > threshold)
        {
            _animator.SetBool("FaceRight", true);
            spriteRenderer.flipX = false;
        }
        else if (_moveInput.x < -threshold)
        {
            _animator.SetBool("FaceLeft", true);
            spriteRenderer.flipX = true;
        }
    }
}
