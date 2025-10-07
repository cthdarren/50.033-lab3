using UnityEngine;

[CreateAssetMenu]
public class PlayerConstants: ScriptableObject
{
    // Movement
    public float moveSpeed = 5;
    public float jumpHangTimeThreshold = 0.1f;
    public float jumpHangGravityScale = 1f;
    public float fallingGravityScale = 5;
    public float defaultGravityScale = 3;
    public float dashDuration = 0.2f;
    public float dashDelay = 0.2f;
    public float dashCooldown = 1f;
    public float dashCooldownTimer = -1f;
    public float dashForce = 33f;
    public float jumpForce = 10f;
    public float maxFallSpeed = 20f;

    // Combat
    public float hitStopDuration = 0.2f;
    public float attackHitboxDelay = 0.2f;
    public float attackHitboxDuration = 0.2f;
}

