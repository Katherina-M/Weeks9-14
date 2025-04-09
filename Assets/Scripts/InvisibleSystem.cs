using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InvisibleSystem : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float invisibilityDuraton = 10f;

    private bool isInvisible = false;
    private float invisibilityTimer = 0f;

    private InvisibilityItem orbSpawner;

    private void Start()
    {
        orbSpawner = GetComponentInChildren<InvisibilityItem>();
        if (orbSpawner != null )
        {
            Debug.Log("InvisibilityItem component not found in children.");
        }
    }

    void Update()
    {
        Vector2 direction = Vector2.zero;

        //Movement Keys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        //If player is invisible, update the timer. Disable invisbility when timer expires.
        if (isInvisible)
        {
            invisibilityTimer -= Time.deltaTime;
            if (invisibilityTimer < 0)
            {
                DeactivateInvisibility();
            }
        }
    }

    //Check Orb pick up by player or not
    private void CheckOrbPickup()
    {
        //Check if Orb exists
        if (orbSpawner != null && orbSpawner.GetCurrentOrb() != null)
        {
            GameObject orb = orbSpawner.GetCurrentOrb();

            //Check for overlap
            if (Vector2.Distance((Vector2)transform.position, (Vector2)orb.transform.position) < 0.1f)
            {
                // Activate invisibility effect.
                ActivateInvisibility();

                // Notify the spawner that the orb has been collected.
                orbSpawner.OrbCollected();

                // Destroy the orb from the scene.
                Destroy(orb);
            }
        }
    }

    //Activate invisibility after collect the orb
    public void ActivateInvisibility()
    {
        Debug.Log("Invisibility Activated!");
        isInvisible = true;
        invisibilityTimer = invisibilityDuraton;
    }

    private void DeactivateInvisibility()
    {
        Debug.Log("Invisibility deactivated!");
        isInvisible = false;
    }
}
