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

    //Enemy spawn amount
    public bool isSpawner = false;
    public int numberToSpawn = 5;
    //Spawn range
    public Vector2 spawnAreaMin = new Vector2(-5f, -5f);
    public Vector2 spawnAreaMax = new Vector2(5f, 5f);
    //Spawn only once
    private static bool hasSpawned = false;

    private void Awake()
    {
        //Register all enemy instance
        allEnemies.Add(this);
        enemyCurrentHP = enemyMaxHP;

        // Attempt to assign the player if not already assigned.
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
        }

        if (isSpawner && !hasSpawned)
        {
            SpawnEnemies();
            hasSpawned = true;
        }

        Debug.Log("Enemy starting HP: " + enemyCurrentHP + "HP");
    }
    private void SpawnEnemies()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            // Generate a random position within the spawn area.
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPos = new Vector2(x, y);

            // Instantiate a new enemy instance
            GameObject newEnemy = Instantiate(this.gameObject, spawnPos, Quaternion.identity);

            // Prevent the cloned enemy from acting as a spawner.
            EnemyHPandAttackManager enemyScript = newEnemy.GetComponent<EnemyHPandAttackManager>();
            if (enemyScript != null)
            {
                enemyScript.isSpawner = false;
            }
        }
    }

    private void OnDestroy()
    {
        // Remove this enemy when it is destroyed.
        allEnemies.Remove(this);
    }

    private void Update()
    {
        // Check if the player is dead by verifying their current HP
        if (player != null)
        {
            PlayerHPandAttackManager playerControl = player.GetComponent<PlayerHPandAttackManager>();
            if (playerControl != null && playerControl.CurrentHP <= 0)
            {
                // If the player is dead, do not attack
            }
        }

        //Updated cooldown time
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }

        //Check if player alive
        if (player != null)
        {
            PlayerHPandAttackManager playerControl = player.GetComponent<PlayerHPandAttackManager>();
            if (playerControl != null && playerControl.CurrentHP <= 0)
            {
                return;
            }
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
        //Stop enemy attack
        if (player == null)
        {
            return;
        }

        //Auto-damage the player when is under the range
        PlayerHPandAttackManager playerControl = player.GetComponent<PlayerHPandAttackManager>();
        
        if (playerControl != null)
        {
            //Damage player
            playerControl.TakeDamage(attackDamage);
            Debug.Log("Enemy attacked the player for " + attackDamage + " damage.");
        }

        // Reset the attack cooldown timer
        attackTimer = attackCooldown;
    }
}
