using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;

    //Enemy spawn area
    public Vector2 spawnAreaMin = new Vector2(-5f, -5f);
    public Vector2 spawnAreaMax = new Vector2(5f, 5f);

    public Transform player;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        // Ensure an enemy prefab is assigned.
        if (enemyPrefab == null)
        {
            return;
        }

        // Loop to instantiate the specified number of enemies.
        for (int i = 0; i < enemyCount; i++)
        {
            // Generate a random position within the defined spawn area.
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(x, y);

            // Instantiate the enemy prefab at the generated position.
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // If a player reference is provided, assign it to the enemy's script.
            EnemyHPandAttackManager enemyScript = newEnemy.GetComponent<EnemyHPandAttackManager>();
            if (enemyScript != null && player != null)
            {
                enemyScript.player = player;
            }
        }
    }
}
