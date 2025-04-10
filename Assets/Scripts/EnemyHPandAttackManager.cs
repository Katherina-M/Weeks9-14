using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class EnemyHPandAttackManager : MonoBehaviour
{
    //All enemy instance list
    public static List<EnemyHPandAttackManager> allEnemies = new List<EnemyHPandAttackManager>();
    //HP
    public int enemyMaxHP = 10;
    private int enemyCurrentHP;

    //Attack
    public int attackDamage = 1;
    public float attackCooldown = 1.0f;
    private float attackTimer = 0f;

    //Check player position, if close to the rank, ennemy will attack
    public Transform player;
    public float attackRange = 1.5f;

    private void Awake()
    {
        //Register all enemy instance
        allEnemies.Add(this);
        enemyCurrentHP = enemyMaxHP;
        Debug.Log("Enemy starting HP: " + enemyCurrentHP + "HP");
    }
    private void OnDestroy()
    {
        // Remove this enemy when it is destroyed.
        allEnemies.Remove(this);
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
        //Enemy die when HP under 0
        enemyCurrentHP -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current HP: " + enemyCurrentHP);

        if (enemyCurrentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Enemy self-destory
        Debug.Log("Enemy died.");

        //Heals players 4 HP each enemy die
        if (player != null)
        {
            PlayerHPandAttackManager playerControl = player.GetComponent<PlayerHPandAttackManager>();

            if (playerControl != null)
            {
                playerControl.playerHeal(playerControl.playerHealAmount);
                Debug.Log("Player healed" + playerControl.playerHealAmount + " points due to enemy kill.");
            }
        }

        Destroy(gameObject);
    }
    private void Attack()
    {
        //Auto-damage the player when is under the range
        PlayerHPandAttackManager playerControl = player.GetComponent<PlayerHPandAttackManager>();
        if (playerControl != null)
        {
            playerControl.TakeDamage(attackDamage);
            Debug.Log("Enemy attacked the player for " + attackDamage + " damage.");
        }
        // Reset the attack cooldown timer
        attackTimer = attackCooldown;
    }
}
