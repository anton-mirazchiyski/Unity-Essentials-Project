using System;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 65f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    
    private bool jumpPressed = false;
    private int jumpCounter = 0;
    private bool isGrounded;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength = 1.3f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        if (isGrounded) jumpCounter = 0;
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            movement = new Vector2(horizontalInput, 0);
            animator.SetBool("isWalking", true);
            RotatePlayer(horizontalInput, 0);
        }
        else
        {
            movement = Vector2.zero;
            animator.SetBool("isWalking", false);
        }
        
        if (Input.GetButtonDown("Jump") && jumpCounter < 2)
        {
            jumpCounter++;
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;

        if (jumpPressed)
        {
            animator.SetBool("isJumping", true);
            Jump();
            jumpPressed = false;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    void Jump()
    {
        rb.AddForce(jumpForce * rb.mass * Vector2.up, ForceMode2D.Impulse);
    }
    
    void RotatePlayer(float x, float y)
    {
        // If there is no input, do not rotate the player
        if (x == 0 && y == 0) return;

        // Calculate the rotation angle based on input direction
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        // Apply the rotation to the player
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
