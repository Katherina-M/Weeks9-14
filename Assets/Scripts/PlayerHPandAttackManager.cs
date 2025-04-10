using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerHPandAttackManager : MonoBehaviour
{
    //HP
    public int playerMaxHP = 20;
    private int playerCurrentHP;
    public int playerHealAmount = 4;

    //Attack
    public int attackDamage = 2;
    public float attackRange = 1.5f;


    private void Start()
    {
        playerCurrentHP = playerMaxHP;
        Debug.Log("Player satrt with" + playerMaxHP + "HP");
    }

    private void Update()
    {
        //Hitiing space bar or left button to attack enemy
        if (Input.GetKeyUp(KeyCode.Space) || (Input.GetMouseButtonDown(0)))
        {
            EnemyHPandAttackManager nearestEnemy = null;
            float closestDistance = float.MaxValue;

            //Check all the enemy in the list if is near by the player
            foreach (EnemyHPandAttackManager enemy in EnemyHPandAttackManager.allEnemies)
            {
                //Calculate their distance between player and the enemy
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < attackRange && distance < closestDistance)
                {
                    closestDistance = distance;
                    //Auto convert nearest enemy as regular enemy
                    nearestEnemy = enemy;
                }
            }

            //If a nearby enemy is deteced, player can cause damage
            if (nearestEnemy != null)
            {
                //Call enemy take damage from EnemyHPandAttackManager
                nearestEnemy.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage (int damage)
    {
        playerCurrentHP -= damage;
        Debug.Log("Player took" + damage + "damage.CurrentHP:" + playerCurrentHP);

        if (playerCurrentHP <= 0)
        {
            Die();
        }
    }

    public void playerHeal (int amount)
    {
        playerCurrentHP += amount;
        if (playerCurrentHP > playerMaxHP)
        {
            playerCurrentHP = playerMaxHP;
        }
        Debug.Log("Player healed " + amount + ". Current HP: " + playerCurrentHP);
    }

    private void Die()
    {
        Debug.Log("Player Died.");

    }


}
