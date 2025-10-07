//using UnityEngine;

//// Honestly might deprecate this for a massive player movement because it
//// feels weird manipulating RB2D in two diff updates
//public class PlayerDash : MonoBehaviour
//{
//    private PlayerInput input;
//    private Animator animator;
//    [SerializeField] private BoolVariable isDashing;
//    [SerializeField] private FloatVariable dashDuration;
//    [SerializeField] private FloatVariable dashForce;
//    [SerializeField] private FloatVariable dashCooldown;
//    [SerializeField] private FloatVariable dashCooldownTimer;
//    [SerializeField] private BoolVariable isInvincible;
//    [SerializeField] private BoolVariable isGrounded;
//    [SerializeField] private BoolVariable isJumping;
//    [SerializeField] private BoolVariable isMovementDisabled;
//    [SerializeField] private FloatVariable jumpForce;
//    [SerializeField] private FloatVariable fallingGravityScale;
//    [SerializeField] private FloatVariable jumpHangTimeThreshold;
//    [SerializeField] private FloatVariable jumpHangGravityScale;
//    [SerializeField] private FloatVariable maxFallSpeed;
//    [SerializeField] private FloatVariable defaultGravityScale;
//    [SerializeField] private FloatVariable moveSpeed;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    public void Start()
//    {
//        input = GetComponent<PlayerInput>();
//        animator = GetComponent<Animator>();
//    }
//    public void Update()
//    {
//        if (isDashing.Value) return;
//        if (dashCooldownTimer.Value > 0) return;
//        if (input.dashInput.WasPressedThisFrame())
//        {
//            isMovementDisabled.Value = true;
//            isDashing.Value = true;
//            isInvincible.Value = true;
//            dashCooldownTimer.Value = dashCooldown.Value;
//            animator.SetTrigger("Dash");
//            //rb.linearVelocity = Vector2.zero;
//            rb.linearVelocity = moveDirectionVector * Vector2.right * dashForce.Value;
//            StartCoroutine(StopTeleportDash());
//        }
//    }

//    public IEnumerator StopTeleportDash()
//    {
//        yield return new WaitForSeconds(dashDuration.Value);
//        animator.SetTrigger("DashEnd");
//        rb.linearVelocity = Vector2.zero; //new Vector2(0, rb.linearVelocityY);
//        isMovementDisabled.Value = false;
//        isDashing.Value = false;
//        isInvincible.Value = false;
//    }
//    //public void OnCollisionEnter2D(Collision2D collision)
//    //{
//    //    if (
//    //        collision.gameObject.CompareTag("Ground") ||
//    //        collision.gameObject.CompareTag("Platform")
//    //    )
//    //    {
//    //        playerData.isJumping = false;
//    //        playerData.isGrounded = true;
//    //    }
//    //}

//    //public void OnCollisionExit2D(Collision2D collision)
//    //{
//    //    if (
//    //        collision.gameObject.CompareTag("Ground") ||
//    //        collision.gameObject.CompareTag("Platform")
//    //    )
//    //    {
//    //        playerData.isGrounded = false;
//    //    }
//    //}
//}
