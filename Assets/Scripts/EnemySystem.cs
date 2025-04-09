using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;
    public float detectionRange = 5f;
    public Transform player;
    public Camera mainCam;

    private Vector2 moveDirection;
    private float timer = 0f;
    private Vector2 minBound;
    private Vector2 maxBound;
    private bool isChasing = false;
    private bool canSeePlayer = true;

    void Start()
    {
        PickNewDirection();
        mainCam = Camera.main;

        //Get Camera boundry
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minBound = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBound = new Vector2(topRight.x, topRight.y);
    }

    
    void Update()
    {
        if (player != null && canSeePlayer)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }
        }
        else if (!canSeePlayer)
        {
            isChasing = false; // Make sure enemy doesn't chase while player is invisible
        }

        if (isChasing)
        {
            //Follow the player
            Vector2 directionToPlayer = (player.position - transform.position);
            Vector3 nextPos = transform.position + (Vector3)(directionToPlayer * moveSpeed * Time.deltaTime);

            //Stay in the camera
            nextPos = ClampToBounds(nextPos);
            transform.position = nextPos;
        }
        else
        {
            Vector3 nextPos = transform.position + (Vector3)(moveDirection * moveSpeed * Time.deltaTime);

            //Check if next position is outside of the camera
            if (nextPos.x < minBound.x || nextPos.x > maxBound.x)
            {
                moveDirection.x *= -1; //Flip x direction
            }
            if (nextPos.y < minBound.y || nextPos.y > maxBound.y)
            {
                moveDirection.y *= -1; //Flip y direction
            }

            //Move enemy in the chosen direction
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            //Pick a new direction when timer expires
            timer += Time.deltaTime;
            if (timer >= changeDirectionTime)
            {
                PickNewDirection();
                timer = 0f;
            }
        }
    }
    void PickNewDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        moveDirection = new Vector2(x, y);
    }

    //Make sure Enemy stay in the camera
    Vector3 ClampToBounds(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minBound.x, maxBound.x);
        pos.y = Mathf.Clamp(pos.y, minBound.y, maxBound.y);
        return pos;
    }

    public void StopTrackingPlayer()
    {
        canSeePlayer = false;
        isChasing = false;
        Debug.Log("Enemy can no longer see the player!");
    }

}
