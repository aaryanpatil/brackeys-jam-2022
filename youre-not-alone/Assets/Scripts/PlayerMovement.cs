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
    CircleCollider2D feetCollider;
    Animator animator;
    

    void Awake() 
    {
        rb2d = GetComponentInChildren<Rigidbody2D>();
        feetCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
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

        if(moveInput.x > 0 || moveInput.x < 0) 
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void FlipSprite()
    {   
        if(moveInput.x < 0 || moveInput.x > 0)
        {
            transform.localScale = new Vector2 (Mathf.Sign(moveInput.x), 1f);
        }
    }

    void OnJump(InputValue value)
    {
        Debug.Log(jumpCount);
        if (value.isPressed && jumpCount == 1)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            jumpCount = 1;
        }
    } 

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            jumpCount = 1;
        }
    } 

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            jumpCount = 0;
        }
    } 

}
