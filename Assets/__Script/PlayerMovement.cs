using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    [Space(10)]
    public float moveInput;
    [Space(10)]
    public float frictionAmmount;

    [Header("Jump")]
    public float jumpForce;
    [Range(0, 1)]
    public float jumpCutMultiplier;
    [Space(10)]
    public float jumpCoyoteTime;
    private float lastGroundedTime;
    public float jumpBufferTime;
    private float lastJumpTime;
    [Space(10)]
    public int fallGravityMultiplier;
    private float gravityScale;
    [Space(10)]
    private bool isJumping;
    private bool grounded;

    [Header("Checks")]
    public Transform groundCheckPoint;
    public Vector2 groundCheckSize;
    [Space(10)]
    public LayerMask groundLayer;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gravityScale = rb.gravityScale;
    }

    void Update()
    {
        #region Inputs
        moveInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKey(KeyCode.Space))     // Space is Down
        {
            lastJumpTime = jumpBufferTime;
        }

        if(Input.GetKeyUp(KeyCode.Space))   // Space is released, so add a float down effect
        {
            OnJumpUp();
        }
        #endregion Inputs

        #region Checks
        if (grounded)   // Checks if on ground
        {
            // if so set the lastGrounded to coyoteTime
            lastGroundedTime = jumpCoyoteTime;
        }

        if (rb.velocity.y < 0)
        {
            isJumping = false;
        }
        #endregion

        #region Jump
        if (lastGroundedTime > 0 && lastJumpTime > 0 && !isJumping && Input.GetKeyDown(KeyCode.Space)) // checks if was last grounded within coyoteTime
        {
            Jump();
        }
        #endregion End Jump

        #region Timer
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
        #endregion timer
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        #region Run
        float targetSpeed = moveInput * moveSpeed;
        // calculate the direction we want to move in and our desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        // calculate the difference between current velocity and desired velocity
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        // change acceleration rate depending on the situation
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        // applies accelertaion to speed difference, then raises to a set power so acceleration increases with higher speed
        // finally multiplies by sign to reapply direction

        rb.AddForce(movement * Vector2.right);
        // applies force to rigidbody, mulitplying by Vector2.right so that it only affects X axis
        #endregion Run

        #region Friction
        // check if we're grounded and that we are trying to stop (not pressing forwards or backwards)
        if (lastGroundedTime > 0 && (!Input.GetKeyDown(KeyCode.D) || !Input.GetKeyDown(KeyCode.A)))
        {
            // then we use either the friction amount (0.2) or our velocity
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmmount));
            // sets to movement direction
            amount *= Mathf.Sign(rb.velocity.x);
            // applies force against movement direction
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion Friction

        #region Jump Gravity
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
        #endregion
    }
    public void OnJump()
    {
        lastJumpTime = jumpBufferTime;
    }

    public void OnJumpUp()  // Floating down
    {
        grounded = false;
        if(rb.velocity.y > 0 && isJumping)
        {
            // reduces current y velocity by amount (0 - 1)
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }

        lastJumpTime = 0;
    }
    private void Jump()
    {
        // apply force, using impulse force mode
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        lastGroundedTime = 0;
        lastJumpTime = 0;
        isJumping = true;
    }
}
