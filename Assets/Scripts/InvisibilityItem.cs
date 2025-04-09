using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static InvisibilityEvent;

public class InvisibilityItem : MonoBehaviour
{
    public Transform player;
    public float pickupRange = 1f;
    public uiManager uiManager;
    public InvisibilityEvent invisibilityEvents;
    public EnemySystem[] enemies;

    private Vector2 minBound, maxBound;
    private Camera mainCam;

    private bool isHidden = false;
    private float hiddenTimer = 0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        mainCam = Camera.main;

        // Get the screen bounds in world space
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minBound = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBound = new Vector2(topRight.x, topRight.y);

        spriteRenderer = GetComponent<SpriteRenderer>();

        SetRandomPosition();

        //Listener Control
        if (invisibilityEvents != null)
        {
            if (uiManager != null)
            {
                invisibilityEvents.OnPlayerInvisible.AddListener(uiManager.OnPlayerBecameInvisible);
            }

            foreach (EnemySystem enemy in enemies)
            {
                if (enemy != null)
                {
                    invisibilityEvents.OnPlayerInvisible.AddListener(enemy.StopTrackingPlayer);
                }
            }
        }
    }

    void Update()
    {
        if (isHidden)
        {
            hiddenTimer -= Time.deltaTime;
            if (hiddenTimer <= 0f)
            {
                SetRandomPosition();
                spriteRenderer.enabled = true;
                isHidden = false;
            }
            return;
        }

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= pickupRange)
            {
                spriteRenderer.enabled = false;
                isHidden = true;
                hiddenTimer = 5f;

                //Listener
                if (invisibilityEvents != null)
                {
                    invisibilityEvents.TriggerInvisibility();
                }

                if (uiManager != null)
                {
                    Debug.Log("uiManager found, applying effect...");
                    uiManager.ApplyInvisibilityEffect();
                }
                else
                {
                    Debug.LogWarning("uiManager is NULL!");
                }
            }
        }
    }

    void SetRandomPosition()
        {
            float x = Random.Range(minBound.x + 0.5f, maxBound.x - 0.5f);
            float y = Random.Range(minBound.y + 0.5f, maxBound.y - 0.5f);
            transform.position = new Vector2(x, y);
        }
}
