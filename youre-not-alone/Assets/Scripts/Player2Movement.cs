using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Movement : MonoBehaviour
{
    [Header("Horizontal Movement")]

    [SerializeField] float runSpeed = 400f;
    [SerializeField] private float smoothInputSpeed = 0.08f;
    [SerializeField] private float smoothClimbInputSpeed = 0.04f;
    [SerializeField] float climbSpeed = 400f;
    [SerializeField] float frozenRunSpeed = 0f;
    [SerializeField] float normalRunSpeed = 400f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 700f;
    int jumpCount = 1;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] float freezeDelay;
    Vector2 moveInput;
    Vector2 climbInput;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    private Vector2 currentClimbInputVector;
    private Vector2 smoothClimbInputVelocity;

    Rigidbody2D rb2d;
    CircleCollider2D feetCollider;
    Animator animator;

    WallInteract wallInteract;
    

    void Awake() 
    {
        rb2d = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        wallInteract = GetComponent<WallInteract>();
    }
    void FixedUpdate()
    { 
        Run();
    }

    private void Update() 
    {
        FlipSprite();
        CancelJumpAnimation();
        if (wallInteract == null) { return; }
        Climb();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Yellow IP");
    }

    void Run()
    {
        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveInput, ref smoothInputVelocity, smoothInputSpeed);
        Vector2 playerVelocity = new Vector2(currentInputVector.x * runSpeed *  Time.fixedDeltaTime, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        if(moveInput.x > 0 || moveInput.x < 0) 
        {
            animator.SetBool("IsRunning", true);
            // animator.SetBool("IsJumping", false);
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

        if (wallInteract.onLeftWall)
        {
            transform.localScale = new Vector2 (-1f, 1f);
        }
        else if (wallInteract.onRightWall)
        {
            transform.localScale = new Vector2 (1f, 1f);
        }
    }

    void OnJump(InputValue value)
    {
        if(wallInteract.onWall) { return; }

        if (value.isPressed && jumpCount == 1 && !wallInteract.onWall)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsRunning", false);
            jumpCount = 0;
        }
        else if (value.isPressed && wallInteract.onWall)
        {
            Debug.Log("Wall Climb"); 
        }
    }

    void OnClimb(InputValue value)
    {
        climbInput = value.Get<Vector2>();
    }

    void CancelJumpAnimation()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;
        if(!playerHasVerticalSpeed)
        {
            animator.SetBool("IsJumping", false);
        }   
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Interactables"))
        {
            
            jumpCount = 1;
            animator.SetBool("IsJumping", false);
        }
        if (other.gameObject.CompareTag("Ice"))
        {
            Debug.Log("Yellow On Ice");
        }
        
        if (other.gameObject.CompareTag("Ice"))
        {
            
            StartCoroutine(FreezePlayer());
            animator.SetBool("IsFrozen", true);
        }
    } 

    IEnumerator FreezePlayer()
    {
        yield return new WaitForSecondsRealtime(freezeDelay);
        runSpeed = frozenRunSpeed;
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Interactables"))
        {
            jumpCount = 1;
            animator.SetBool("IsJumping", false);
        }
    } 

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Interactables"))
        {
            jumpCount = 0;
        }

        if (other.gameObject.CompareTag("Ice"))
        {
            runSpeed = normalRunSpeed;
            animator.SetBool("IsFrozen", false);
        }
    } 

    void Climb()
    {
        if(wallInteract == null) { return; }

        if (wallInteract.onWall)
        {
            animator.SetBool("IsClimbing", true);
            currentClimbInputVector = Vector2.SmoothDamp(currentClimbInputVector, climbInput, ref smoothClimbInputVelocity, smoothClimbInputSpeed);
            Vector2 playerVelocity = new Vector2(rb2d.velocity.x, currentClimbInputVector.y * climbSpeed *  Time.fixedDeltaTime);
            rb2d.velocity = playerVelocity;
        }
        else if(!wallInteract.onWall)
        {
            animator.SetBool("IsClimbing", false);
        }
    }
}
