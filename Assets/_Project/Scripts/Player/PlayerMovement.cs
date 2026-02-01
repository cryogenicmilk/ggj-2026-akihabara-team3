using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

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

        UpdateDirection();
    }

    void UpdateDirection()//ˆÚ“®‚·‚é‚Æ‚«‚ÉƒTƒCƒY‚ª•Ï‚í‚Á‚Ä‚½‚Ì‚ÅC³
    {
        if (_moveInput.x > 0)
            spriteRenderer.flipX = false;
        else if (_moveInput.x < 0)
            spriteRenderer.flipX = true;
    }
}
