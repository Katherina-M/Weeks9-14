using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPandAttackManager : MonoBehaviour
{
    //HP
    public int enemyMaxHP = 10;
    private int enemyCurrentHP;

    //Attack
    public int attackDamage = 2;
    public float attackCooldown = 1.0f;
    private float attackTimer = 0f;

    //Check player position, if close to the rank, ennemy will attack
    public Transform player;
    public float attackRange = 1.5f;

    private void Start()
    {
        enemyCurrentHP = enemyMaxHP;
        Debug.Log("Enemy starting HP: " + enemyCurrentHP);
    }

    private void Update()
    {
        //Updated cooldown time
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }

        //Check player under attack range or not
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            //Debug.Log("Distance to player: " + distance);
            if (distance <= attackRange && attackTimer <= 0f)
            {
                Debug.Log("Player in range, attempting attack");
                Attack();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHP -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current HP: " + enemyCurrentHP);

        if (enemyCurrentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
    private void Attack()
    {
        PlayerControlManager playerControl = player.GetComponent<PlayerControlManager>();
        if (playerControl != null)
        {
            playerControl.TakeDamage(attackDamage);
            Debug.Log("Enemy attacked the player for " + attackDamage + " damage.");
        }
        // Reset the attack cooldown timer
        attackTimer = attackCooldown;
    }
}
