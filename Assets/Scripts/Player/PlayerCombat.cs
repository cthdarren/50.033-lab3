using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Collider2D attackHitbox;
    public PlayerInput input;
    public GameEvent attackEvent;
    [SerializeField] private FloatVariable hp;
    [SerializeField] private BoolVariable isInvincible;
    [SerializeField] private BoolVariable isDashing;
    [SerializeField] private FloatVariable damage;
    [SerializeField] private GameEvent attackedEnemy;

    public void HandleCombat()
    {
        if (isDead())
        {
            // Play death animation + ui screen
        }
        if (input.attackInput.WasPressedThisFrame())
        {
            Attack();
        }
    }
    public bool isDead()
    {
        return hp.Value < 0;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible.Value) return;
        hp.Value -= damage;
    }

    public void Heal(float health)
    {
        hp.Value += health;
    }

    public void Attack()
    {
        if (isDashing.Value) return;
        animator.SetTrigger("Attack");
        attackEvent.Raise();
    }

    public void EnableAttackHitboxWindow()
    {
        attackHitbox.enabled = true;
        var results = new Collider2D[10];
        int count = attackHitbox.Overlap(new ContactFilter2D().NoFilter(), results);
        if (count > 0)
        {
            foreach (Collider2D collider in results)
            {
                Debug.Log("Attacked enemy");
                if (collider && collider.CompareTag("Enemy"))
                {
                    Debug.Log(collider);
                    EnemyAI enemy = collider.GetComponent<EnemyAI>();
                    if (enemy != null)
                    {
                        attackedEnemy.Raise();
                        enemy.TakeDamage(damage.Value);
                    }
                }
            }
        }
    }

    public void DisableAttackHitBoxWindow()
    {
        attackHitbox.enabled = false;
    }
}
