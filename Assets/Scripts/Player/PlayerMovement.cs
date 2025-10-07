using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public PlayerInput input;
    public Rigidbody2D rb;
    [SerializeField] private PerfectDodge perfectDodge;
    [SerializeField] private BoolVariable isInvincible;
    [SerializeField] private BoolVariable isGrounded;
    [SerializeField] private BoolVariable isJumping;
    [SerializeField] private BoolVariable isDashing;
    [SerializeField] private BoolVariable isMovementDisabled;
    [SerializeField] private FloatVariable jumpForce;
    [SerializeField] private FloatVariable fallingGravityScale;
    [SerializeField] private FloatVariable jumpHangTimeThreshold;
    [SerializeField] private FloatVariable jumpHangGravityScale;
    [SerializeField] private FloatVariable maxFallSpeed;
    [SerializeField] private FloatVariable defaultGravityScale;
    [SerializeField] private FloatVariable moveSpeed;
    [SerializeField] private FloatVariable dashDuration;
    [SerializeField] private FloatVariable dashForce;
    [SerializeField] private FloatVariable dashCooldown;
    [SerializeField] private FloatVariable dashCooldownTimer;

    public float moveDirectionVector = 1;

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    public void FixedUpdate()
    {

        if (isDashing.Value)
        {
            rb.gravityScale = 0;
        }
        else if (!isGrounded.Value)
        {

            if (rb.linearVelocityY <= 0)
            {
                rb.gravityScale = fallingGravityScale.Value;
                // For capping max falling speeds
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed.Value));
            }
        }
        else if (isGrounded.Value)
        {
            isJumping.Value = false;
            rb.gravityScale = defaultGravityScale.Value;
        }
        // Extend air time slightly between threshold
        else if (isJumping.Value && Mathf.Abs(rb.linearVelocityY) < jumpHangTimeThreshold.Value)
            rb.gravityScale = jumpHangGravityScale.Value;

    }

    public void HandleMovement()
    {
        if (isMovementDisabled.Value) return;

        HandleFaceDirection();
        HandleJump();

        if (input.wasdInputVector.WasReleasedThisFrame())
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
        if (input.wasdInputVector.IsPressed())
        {
            // W+A = 0.707 x 
            moveDirectionVector = Mathf.Round(input.wasdInputVector.ReadValue<Vector2>().x);

            //if (input.wasdInputVector.ReadValue<Vector2>().y <= 0)
            rb.linearVelocity = new Vector2(moveSpeed.Value * moveDirectionVector, rb.linearVelocityY);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
        if (input.jumpInput.ReadValue<float>() >= 1)
            isJumping.Value = true;

        HandleDash();
        HandleAnimations();
    }
    public void HandleAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("VerticalSpeed", rb.linearVelocityY);
        animator.SetBool("isGrounded", isGrounded.Value);
    }

    public void HandleFaceDirection()
    {
        if (animator.GetBool("IsAttacking")) return;
        Vector3 vector3scale = this.transform.localScale;
        if (Mathf.Abs(moveDirectionVector) >= 1)
            this.transform.localScale = new Vector3(moveDirectionVector * Mathf.Abs(vector3scale.x), vector3scale.y, vector3scale.z);
    }

    public void HandleJump()
    {
        if (input.jumpInput.WasReleasedThisFrame() && !isGrounded.Value)
        {
            float linearYtoSet = rb.linearVelocityY > 0 ? 0 : rb.linearVelocityY;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, linearYtoSet);
        }

        if (input.jumpInput.WasPressedThisFrame())
        {
            if (isGrounded.Value)
            {
                rb.AddForce(new Vector2(0, jumpForce.Value), ForceMode2D.Impulse);
                isJumping.Value = true;
                isGrounded.Value = false;
            }
        }

    }

    public void HandleDash()
    {
        if (isDashing.Value) return;
        if (dashCooldownTimer.Value > 0) return;
        if (input.dashInput.WasPressedThisFrame())
        {
            isMovementDisabled.Value = true;
            isDashing.Value = true;
            isInvincible.Value = true;
            dashCooldownTimer.Value = dashCooldown.Value;
            perfectDodge.DropHitbox();
            animator.SetTrigger("Dash");
            // enable hitbox that is on current player fixed to world space for perfectDodgeWindow seconds
            // hitbox ontrigger2D separate script
            // move hitbox back to follow player hitbox
            rb.linearVelocity = moveDirectionVector * Vector2.right * dashForce.Value;
            StartCoroutine(StopTeleportDash());
        }
    }

    public IEnumerator StopTeleportDash()
    {
        yield return new WaitForSeconds(dashDuration.Value);
        animator.SetTrigger("DashEnd");
        rb.linearVelocity = Vector2.zero; //new Vector2(0, rb.linearVelocityY);
        isMovementDisabled.Value = false;
        isDashing.Value = false;
        isInvincible.Value = false;
    }
}
