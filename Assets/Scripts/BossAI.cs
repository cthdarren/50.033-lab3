using UnityEngine;
public class BossAI : EnemyAI
{
    protected override void AttackState()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isWalking", false);

        if (Vector2.Distance(transform.position, playerTransform.position) > enemyData.attackRange)
        {
            currentState = AIState.Chasing;
            return;
        }

        if (attackTimer <= 0 && !isAttacking)
        {
            Animator animator = GetComponent<Animator>();

            int randomAttackID = Random.Range(0, 3);
            animator.SetInteger("AttackId", randomAttackID);

            animator.SetTrigger("Attack");

            attackTimer = enemyData.attackCooldown;
        }
    }
}