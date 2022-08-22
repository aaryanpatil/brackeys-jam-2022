using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]

    [SerializeField] float runSpeed = 400f;
    [SerializeField] private float smoothInputSpeed = 0.08f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 700f;
    int jumpCount = 1;

    Vector2 moveInput;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    Rigidbody2D rb2d;
    

    void Awake() 
    {
        rb2d = GetComponentInChildren<Rigidbody2D>();
    }
    void FixedUpdate()
    { 
        Run();
    }

    private void Update() 
    {
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveInput, ref smoothInputVelocity, smoothInputSpeed);
        Vector2 playerVelocity = new Vector2(currentInputVector.x * runSpeed *  Time.fixedDeltaTime, rb2d.velocity.y);
        rb2d.velocity = playerVelocity; 
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && jumpCount == 1)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }
    }

    private void FlipSprite()
    {   
        if(moveInput.x < 0 || moveInput.x > 0)
        {
            transform.localScale = new Vector2 (Mathf.Sign(moveInput.x), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");
            jumpCount = 1;
        }
    } 
}
