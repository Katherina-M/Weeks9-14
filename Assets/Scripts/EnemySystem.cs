using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;

    private Vector2 moveDirection;
    private float timer = 0f;
   
    void Start()
    {
        PickNewDirection();
    }

    
    void Update()
    {
        //Move enemy in the chosen direction
        transform.Translate (moveDirection * moveSpeed * Time.deltaTime);

        //Pick a new direction when timer expires
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            PickNewDirection();
            timer = 0f;
        }
    }
    void PickNewDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        moveDirection = new Vector2(x, y);
    }
}
