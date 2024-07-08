using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int damage = 50;
    [SerializeField] private float damageRange = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    private Animator animator;
    private Vector2 attackDirection = Vector2.down;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //change player facing direction
        if (Input.GetKeyDown(KeyCode.D))
        {
            attackDirection = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            attackDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            attackDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            attackDirection = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        animator.SetBool("isAttacking", true);

        //detect all enemies in front of the player
        Vector2 attackPoint = (Vector2)transform.position + attackDirection * damageRange;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, damageRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(damage);
            }
        }
    }

    public void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    void OnDrawGizmosSelected()
    {
        if (transform == null)
            return;

        Gizmos.color = Color.red;
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * damageRange;
        Gizmos.DrawWireSphere(attackPosition, damageRange);
    }
}
