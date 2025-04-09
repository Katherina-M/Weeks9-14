using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public int maxHP = 20;
    private int currentHP;


    private void Start()
    {
        currentHP = maxHP;
        Debug.Log("Player satrt with 20HP");
    }

    public void TakeDamage (int damage)
    {
        currentHP -= damage;
        Debug.Log("Player took" + damage + "damage.CurrentHP:" + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal (int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        Debug.Log("Player healed " + amount + ". Current HP: " + currentHP);
    }

    private void Die()
    {
        Debug.Log("Player Died.");
    }


}
