using UnityEngine;

[CreateAssetMenu]
// TODO Refactor to use PlayerConstants and PlayerStates eventually in actual game
public class PlayerState: ScriptableObject
{
    // States
    public bool isJumping = false;
    public bool isDashing = false;
    public bool isGrounded = true;
    public bool isInvincible = false;
    public bool isMovementDisabled = false;
    public bool isAggroed = false;
}

