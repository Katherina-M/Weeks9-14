using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    //HP
    public int playerMaxHP = 20;
    private int playerCurrentHP;


    private void Start()
    {
        playerCurrentHP = playerMaxHP;
        Debug.Log("Player satrt with 20HP");
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

    public void Heal (int amount)
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
