using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibilityItem : MonoBehaviour
{
    public Transform player;
    public float pickupRange = 1f;

    private bool hasSetPosition = false;
    private Vector2 minBound, maxBound;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        // Get the screen bounds in world space
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minBound = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBound = new Vector2(topRight.x, topRight.y);

        //Place the orb randomly
        if (!hasSetPosition)
        {
            float x = Random.Range(minBound.x + 0.5f, maxBound.x - 0.5f);
            float y = Random.Range(minBound.y + 0.5f, maxBound.y - 0.5f);
            transform.position = new Vector2(x, y);
            hasSetPosition = true;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= pickupRange)
            {
                Destroy(gameObject); // Bye bye, orb!
            }
        }
    }
}
