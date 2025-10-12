using System;
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

    public PlayerStateSerialized Serialize()
    {
        return new PlayerStateSerialized
        {
            isJumping = isJumping,
            isDashing = isDashing,
            isGrounded = isGrounded,
            isInvincible = isInvincible,
            isMovementDisabled = isMovementDisabled,
            isAggroed = isAggroed
        };
    }
}

[Serializable]
public struct PlayerStateSerialized
{
    public bool isJumping;
    public bool isDashing;
    public bool isGrounded;
    public bool isInvincible;
    public bool isMovementDisabled;
    public bool isAggroed;
}

