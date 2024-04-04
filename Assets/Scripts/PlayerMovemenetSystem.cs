using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovementSystem : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public InputAction playerControls;
    public static event Action OnPlayerJump;

    [SerializeField] Animator mainAnimator;
    [SerializeField] int jumpPower;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;
    [SerializeField] float jumpTime;
    [SerializeField] float speed;


    private Vector2 vecGravity;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isJumping;
    private float jumpCounter;


    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }


    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    private void Update()
    {
        float move = horizontal * speed;
        playerRb.velocity = new Vector2(move, playerRb.velocity.y);

        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        if (playerRb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            playerRb.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }


        if (move > 0 || move < 0)
        {
            mainAnimator.SetBool("move", true);
        }
        else
        {
            mainAnimator.SetBool("move", false);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (!isJumping && isGrounded())
        {
            mainAnimator.SetBool("jump", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
       
        if (context.started && isGrounded())
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
            mainAnimator.SetBool("jump", true);
            isJumping = true;
            jumpCounter = 0;
            OnPlayerJump?.Invoke();
        }

        if (context.canceled)
        {
                isJumping = false; // Reset the isJumping flag
                jumpCounter = 0;

                // Apply jump velocity reduction
                if (playerRb.velocity.y > 0)
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * 0.6f);
                }
               
        }

        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;

        }
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(3f, 0.15f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}