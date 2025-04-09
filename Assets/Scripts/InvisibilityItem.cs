using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibilityItem : MonoBehaviour
{
    public GameObject orb;
    private GameObject currentOrb;

    public float spawnInterval = 5f;

    private Camera mainCam;
    private Vector2 minBound;
    private Vector2 maxBound;

    //  Located the current orb
    public GameObject GetCurrentOrb()
    {
        Debug.Log("Returning the current orb.");
        return currentOrb;
    }

    //Updaqte the current orb
    private void SetCurrentOrb(GameObject newOrb)
    {
        if (currentOrb != newOrb)
        {
            Debug.Log("Updating the current orb.");
            currentOrb = newOrb;
        }
        else
        {
            Debug.Log("No update performed.");
        }
    }

    void Start()
    {
        mainCam = Camera.main;

        //Get Camera boundry
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minBound = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBound = new Vector2(topRight.x, topRight.y);

        // start the Coroutine
        StartCoroutine(SpawnOrbRoutine());
    }

    IEnumerator SpawnOrbRoutine()
    {
        while (true)
        {
            //Only Spawn one orb
            if (currentOrb == null)
            {
                //Wait for re-spawn
                yield return new WaitForSeconds(spawnInterval);

                //Generate random position
                float randomX = Random.Range(minBound.x, maxBound.x);
                float randomY = Random.Range(minBound.y, maxBound.y);
                Vector2 spawnPosition = new Vector2(randomX, randomY);

                //Random orb position, refer to the current exist orb
                GameObject spawnedOrb = Instantiate(orb, spawnPosition, Quaternion.identity);
                SetCurrentOrb(spawnedOrb);
            }

            yield return null;
        }
    }
    public void OrbCollected()
    {
        SetCurrentOrb(null);
    }
}
